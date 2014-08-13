﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GeneticAlgorithms;
using GeneticAlgorithms.ExampleClasses;
using GeneticAlgorithms.ExampleClasses.MondLandungs;
using ZedGraph;

namespace GeneticAlgorithmsGUI_EN
{
    public partial class GUI : Form
    {
        private PointPairList avgFitnessList, avgLengthList, avgVariationList, maxFitnessList, minFitnessList;
        private LineItem avgFitnessCurve, avgLengthCurve, avgVariationCurve, maxFitnessCurve, minFitnessCurve;
        private GeneticSimulation<IntGene> GenSim = null;
        private MondlandungsSimulation MoonSim = null;
        private IRecombinationProvider recombinationProvider = null;
        private ISelectionProvider selectionProvider = null;
        private Bitmap _backBuffer = null;
        private Graphics gBuffer = null;
        private Bitmap bmpSpace = null;
        private Bitmap bmpSpaceshipIntact = null;
        private Bitmap bmpSpaceshipBroken = null;
        private Bitmap bmpSpaceship = null;
        private Bitmap bmpSpaceshipEmpty = null;
        private Bitmap bmpEngine = null;
        private Bitmap bmpSpaceman = null;
        private Bitmap bmpFlag = null;
        private bool simulationAbort = false;
        private bool closingApplication = false;

        private int finalHeight = 0;

        private int turn = 0;

        /// <summary>
        /// Constructor - Initialisation of the UI
        /// </summary>
        public GUI()
        {
            InitializeComponent();
            // Initialisation of the ZedGraph
            GraphPane graphPane = zgc_Simulationsgraph.GraphPane;
            graphPane.Title.Text = "Population";
            graphPane.XAxis.Title.Text = "Rounding";
            graphPane.XAxis.Scale.Min = 1;
            graphPane.XAxis.Scale.MaxAuto = true;
            graphPane.YAxis.Title.Text = "Fitness";
            graphPane.YAxis.Scale.Max = 1.0;
            graphPane.YAxis.Scale.Min = 0.0;
            graphPane.YAxisList.Add("Length");
            graphPane.YAxisList[1].IsVisible = false;
            graphPane.YAxisList[1].Scale.MaxAuto = true;
            graphPane.YAxisList[1].Scale.Min = 0;

            // Initialisation of the Graph
            this.avgFitnessList = new PointPairList();
            this.avgLengthList = new PointPairList();
            this.avgVariationList = new PointPairList();
            this.maxFitnessList = new PointPairList();
            this.minFitnessList = new PointPairList();
            this.avgFitnessCurve = graphPane.AddCurve("Ø Fitness", avgFitnessList, Color.Green, SymbolType.None);
            this.avgLengthCurve = graphPane.AddCurve("Ø Length", avgLengthList, Color.Purple, SymbolType.None);
            this.minFitnessCurve = graphPane.AddCurve("min. Fitness", minFitnessList, Color.Red, SymbolType.None);
            this.maxFitnessCurve = graphPane.AddCurve("max. Fitness", maxFitnessList, Color.Blue, SymbolType.None);
            avgLengthCurve.YAxisIndex = 1;
            avgFitnessCurve.IsVisible = true;
            avgLengthCurve.IsVisible = false;
            minFitnessCurve.IsVisible = false;
            maxFitnessCurve.IsVisible = false;

            // Initialisation of the ComboBox
            cmb_Recombinator.SelectedIndex = 0;
            recombinationProvider = new AsymmetricCrossoverRecombinator();
            cmb_Selector.SelectedIndex = 1;
            selectionProvider = new PieCakeSelector();

            // Assembly Creation
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream stream = null;

            // Load Pictures from the Assembly Resources
            stream = myAssembly.GetManifestResourceStream("GeneticAlgorithmsGUI_EN.Space.bmp");
            bmpSpace = new Bitmap(stream);
            stream = myAssembly.GetManifestResourceStream("GeneticAlgorithmsGUI_EN.Spaceship.gif");
            bmpSpaceshipIntact = new Bitmap(stream);
            stream = myAssembly.GetManifestResourceStream("GeneticAlgorithmsGUI_EN.Explosion.gif");
            bmpSpaceshipBroken = new Bitmap(stream);
            stream = myAssembly.GetManifestResourceStream("GeneticAlgorithmsGUI_EN.Engine.gif");
            bmpEngine = new Bitmap(stream);
            stream = myAssembly.GetManifestResourceStream("GeneticAlgorithmsGUI_EN.SpaceshipEmpty.gif");
            bmpSpaceshipEmpty = new Bitmap(stream);
            stream = myAssembly.GetManifestResourceStream("GeneticAlgorithmsGUI_EN.Spaceman.gif");
            bmpSpaceman = new Bitmap(stream);
            stream = myAssembly.GetManifestResourceStream("GeneticAlgorithmsGUI_EN.Flag.png");
            bmpFlag = new Bitmap(stream);

            // Initialisation of the DoubleBuffer
            _backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            gBuffer = Graphics.FromImage(_backBuffer);
            gBuffer.Clear(Color.White);
        }


        /// <summary>
        /// Paint Event
        /// </summary>
        /// <param name="sender">Caller Object</param>
        /// <param name="e">Event Object</param>
        private void pnl_Animation_Paint(object sender, PaintEventArgs e)
        {
            drawClearBackground();
        }


        /// <summary>
        /// empty Space-/Moonscape draw
        /// </summary>
        private void drawClearBackground()
        {
            gBuffer.DrawImage(bmpSpace, 1, 1, 300, 650);
            pnl_Animation.CreateGraphics().DrawImageUnscaled(_backBuffer, 0, 0);
        }

        /// <summary>
        /// Spaceship Image on the Background in place at certain level
        /// </summary>
        /// <param name="height">Height of the Spaceship</param>
        /// <param name="thrust">Given Thrust</param>
        public void setSpaceship(int height, int thrust)
        {
            gBuffer.DrawImage(bmpSpace, 0, 0, 300, 650);
            float yPosition = 550.0f/Convert.ToInt64(txt_Height.Text);
            yPosition *= height;
            yPosition = 550 - yPosition;
            int scaledThrust = thrust * 2;

            gBuffer.DrawImage(bmpEngine, 150 - (scaledThrust / 2), yPosition + 65, scaledThrust, scaledThrust);
            gBuffer.DrawImage(bmpSpaceship, 100, Convert.ToInt32(yPosition), 100, 100);
            pnl_Animation.CreateGraphics().DrawImageUnscaled(_backBuffer, 0, 0);
        }

        /// <summary>
        /// End the Application
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// "About"-ViewDialogBox
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Mondlandungs-Simulation mit genetischen Algorithmen\n\nVersion 1.0\n\nEntwickelt von Daniel Baulig, Jonas Heil, Christian Kleemann, Sven Sperner\n\nZedGraph Control von http://www.zedgraph.org unter LGPL", "Über");
        }


        /// <summary>
        /// Validation of an input integer parameter
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void txt_Int_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToInt32((sender as TextBox).Text) < 0)
                    throw new FormatException();
            }
            catch (FormatException exception)
            {
                MessageBox.Show("No positive integer!");
                (sender as TextBox).SelectAll();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Validation of an input floating-point parameter
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void txt_Float_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToDouble((sender as TextBox).Text) < 0)
                    throw new FormatException();
            }
            catch (FormatException exception)
            {
                MessageBox.Show("No positive floating point value!");
                (sender as TextBox).SelectAll();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Hide average fitness of the population
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void chk_AVGFitness_CheckedChanged(object sender, EventArgs e)
        {
            avgFitnessCurve.IsVisible = (sender as CheckBox).Checked;
            zgc_Simulationsgraph.GraphPane.YAxisList[0].IsVisible = chk_AVGFitness.Checked || chk_maxFitness.Checked || chk_minFitness.Checked;
            zgc_Simulationsgraph.AxisChange();
            zgc_Simulationsgraph.Invalidate();
        }

        /// <summary>
        /// Toggle average chromosome length of the population
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void chk_Duration_CheckedChanged(object sender, EventArgs e)
        {
            avgLengthCurve.IsVisible = (sender as CheckBox).Checked;
            zgc_Simulationsgraph.GraphPane.YAxisList[1].IsVisible = avgLengthCurve.IsVisible = (sender as CheckBox).Checked;
            zgc_Simulationsgraph.AxisChange();
            zgc_Simulationsgraph.Invalidate();
        }

        /// <summary>
        /// Hide Minimum fitness of the population
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void chk_minFitness_CheckedChanged(object sender, EventArgs e)
        {
            minFitnessCurve.IsVisible = (sender as CheckBox).Checked;
            zgc_Simulationsgraph.GraphPane.YAxisList[0].IsVisible = chk_AVGFitness.Checked || chk_maxFitness.Checked || chk_minFitness.Checked;
            zgc_Simulationsgraph.AxisChange();
            zgc_Simulationsgraph.Invalidate();
        }

        /// <summary>
        /// Hide Maximum fitness of the population
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void chk_maxFitness_CheckedChanged(object sender, EventArgs e)
        {
            maxFitnessCurve.IsVisible = (sender as CheckBox).Checked;
            zgc_Simulationsgraph.GraphPane.YAxisList[0].IsVisible = chk_AVGFitness.Checked || chk_maxFitness.Checked || chk_minFitness.Checked;
            zgc_Simulationsgraph.AxisChange();
            zgc_Simulationsgraph.Invalidate();
        }

        /// <summary>
        /// Reset the simulation results
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void btn_Zuruecksetzten_Click(object sender, EventArgs e)
        {
            avgLengthList.Clear();
            avgVariationList.Clear();
            avgFitnessList.Clear();
            maxFitnessList.Clear();
            minFitnessList.Clear();
            GenSim = null;
            MoonSim = null;
            turn = 0;
            cmb_Selector.Enabled = true;
            cmb_Recombinator.Enabled = true;
            txt_ChromosomeLength.Enabled = true;
            txt_Weight.Enabled = true;
            txt_Height.Enabled = true;
            txt_Engine.Enabled = true;
            txt_MutationsRate.Enabled = true;
            txt_LossRate.Enabled = true;
            txt_DuplicationsRate.Enabled = true;
            dgv_Population.Rows.Clear();
            btn_Play.Enabled = false;
            lbl_SpeedValue.Text = "0";
            lbl_HeightValue.Text = "0";
            lbl_ThrustValue.Text = "0";
            lbl_TankValue.Text = "0";
            chk_AVGFitness.Checked = true;
            chk_maxFitness.Checked = false;
            chk_minFitness.Checked = false;
            chk_Length.Checked = false;
            chk_Live.Checked = false;
            zgc_Simulationsgraph.GraphPane.GraphObjList.Clear();
            zgc_Simulationsgraph.AxisChange();
            zgc_Simulationsgraph.Invalidate();
            drawClearBackground();
            GraphPane p = zgc_Simulationsgraph.GraphPane;
            zgc_Simulationsgraph.ZoomOutAll(p);
            p.YAxisList[1].Scale.Max = 10;
            p.YAxisList[1].Scale.MaxAuto = true;

        }

        /// <summary>
        /// Go through the simulation rounds
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void OnSimulationTurn(object sender, EventArgs e)
        {
            turn ++;
            if (simulationAbort)
            {
                GenSim.AbortSimulation();
                simulationAbort = false;
            }
            this.avgFitnessList.Add(turn, GenSim.AverageFitness);
            this.avgLengthList.Add(turn, GenSim.AverageChromosomeLength);
            if (zgc_Simulationsgraph.GraphPane.YAxisList[1].Scale.Max < GenSim.AverageChromosomeLength)
                zgc_Simulationsgraph.GraphPane.YAxisList[1].Scale.Max = GenSim.AverageChromosomeLength + 1;
            this.maxFitnessList.Add(turn, GenSim.MostSuccessfulIndividual.Fitness);
            this.minFitnessList.Add(turn, GenSim.LeastSuccessfulIndividual.Fitness);
            if (turn % 10 == 0 && chk_Live.Checked)
            {
                zgc_Simulationsgraph.AxisChange();
                zgc_Simulationsgraph.Invalidate();
            }
            Application.DoEvents();
        }

        /// <summary>
        /// Simulation start
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void btn_Simuliere_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            btn_Simulate.Enabled = false;
            btn_Reset.Enabled = false;
            btn_AutoSim.Enabled = false;
            btn_SimAbort.Focus();
            //erster Durchlauf der Simulation
            if (GenSim == null)
            {
                btn_Play.Enabled = true;
                txt_ChromosomeLength.Enabled = false;
                cmb_Recombinator.Enabled = false;
                cmb_Selector.Enabled = false;
                txt_Weight.Enabled = false;
                txt_Height.Enabled = false;
                txt_Engine.Enabled = false;
                txt_MutationsRate.Enabled = false;
                txt_LossRate.Enabled = false;
                txt_DuplicationsRate.Enabled = false;
                IntGene.MaxValue = Convert.ToInt32(txt_Engine.Text);
                MoonSim = new MondlandungsSimulation(Convert.ToInt32(txt_Height.Text), Convert.ToInt32(txt_Engine.Text), Convert.ToInt32(txt_Weight.Text), tsmi_SpacemanHeight.Checked);
                GenSim = new GeneticSimulation<IntGene>(100, Convert.ToInt32(txt_ChromosomeLength.Text), MoonSim, recombinationProvider, selectionProvider);
                GenSim.SimulationTurn += OnSimulationTurn;
                GenSim.GeneMutationRate = Convert.ToDouble(txt_MutationsRate.Text);
                GenSim.GeneDuplicationRate = Convert.ToDouble(txt_DuplicationsRate.Text);
                GenSim.GeneDropRate = Convert.ToDouble(txt_LossRate.Text);
            }
            //automatisierte Simulation bis Erreichen der Delta-Fitness
            if (sender == btn_AutoSim)
            {
                btn_SimAbort.Focus();
                float fitnessGrenze = Convert.ToSingle(txt_Fitness.Text);
                float startFitness = GenSim.AverageFitness;
                while (fitnessGrenze + startFitness > GenSim.AverageFitness && !simulationAbort)
                {
                    Application.DoEvents();
                    GenSim.RunSimulation();
                }
                simulationAbort = false;
                btn_Simulate.Enabled = true;
                btn_Reset.Enabled = true;
                btn_AutoSim.Enabled = true;
            }
            //einfache Simulation mit angegebener Rundenzahl
            else
                GenSim.RunSimulation(Convert.ToInt32(txt_RoundNumber.Text));

            if (!closingApplication)
            {
                dgv_Population.Rows.Clear();

                dgv_Population.Rows.Add(GenSim.PopulationSize);
                for (int i = 0; i < GenSim.PopulationSize; i++)
                {
                    dgv_Population.Rows[i].Cells[0].Value = GenSim[i].GeneCount.ToString();
                    dgv_Population.Rows[i].Cells[1].Value = GenSim[i].ToString();
                    dgv_Population.Rows[i].Cells[2].Value = GenSim[i].Fitness.ToString();
                    dgv_Population.Rows[i].Tag = GenSim[i];
                }

                zgc_Simulationsgraph.AxisChange();
                zgc_Simulationsgraph.Invalidate();
                Cursor = Cursors.Default;
                btn_Simulate.Enabled = true;
                btn_Reset.Enabled = true;
                btn_AutoSim.Enabled = true;
            }
        }

        /// <summary>
        /// Selected selector
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void cmb_Selektor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0: // Alpha
                    selectionProvider = new AlphaSelector();
                    break;
                case 1: // Pie Cake
                    selectionProvider = new PieCakeSelector();
                    break;
                case 2: // Random
                    selectionProvider = new RandomSelector();
                    break;
                default:
                    selectionProvider = new PieCakeSelector();
                    break;
            }
        }

        /// <summary>
        /// Selected recombinator
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void cmb_Rekombinator_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0: // Crossover
                    recombinationProvider = new AsymmetricCrossoverRecombinator();
                    break;
                case 1: // Zip
                    recombinationProvider = new AsymmetricZipRecombinator();
                    break;
                default:
                    recombinationProvider = new AsymmetricCrossoverRecombinator();
                    break;
            }
        }

        /// <summary>
        /// Draw spaceman and spaceship at a certain level on the background
        /// </summary>
        /// <param name="x">Height</param>
        private void setSpaceman(int x)
        {
            setSpaceship(0, 0);
            gBuffer.DrawImage(bmpSpaceman, x, 590, 25, 50);
            pnl_Animation.CreateGraphics().DrawImageUnscaled(_backBuffer, 0, 0);
        }

        /// <summary>
        /// Moon touchdown simulations round through
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void OnMondlandungsSimulationTurn(object sender, EventArgs e)
        {
            MondlandungsSimulationEventArgs mondlandungsArgs = e as MondlandungsSimulationEventArgs;
            lbl_SpeedValue.Text = Convert.ToString(mondlandungsArgs.Raumschiff.Geschwindigkeit);
            lbl_HeightValue.Text = Convert.ToString(mondlandungsArgs.Raumschiff.Hoehe);
            lbl_ThrustValue.Text = Convert.ToString(mondlandungsArgs.Schub);
            lbl_TankValue.Text = Convert.ToString(mondlandungsArgs.Raumschiff.Treibstoff);
            Application.DoEvents();
            if (!closingApplication)
            {
                float floatHoehe = finalHeight;
                //Raumschiff fliegt aufwärts
                if (mondlandungsArgs.Raumschiff.Geschwindigkeit > 0)
                    while (floatHoehe < mondlandungsArgs.Raumschiff.Hoehe)
                    {
                        floatHoehe += mondlandungsArgs.Raumschiff.Geschwindigkeit / 10.0f;
                        System.Threading.Thread.Sleep(10);
                        setSpaceship(Convert.ToInt32(floatHoehe), mondlandungsArgs.Schub);
                    }
                //Raumschiff fliegt abwärts
                else
                    while (floatHoehe > mondlandungsArgs.Raumschiff.Hoehe)
                    {
                        floatHoehe += mondlandungsArgs.Raumschiff.Geschwindigkeit / 10.0f;
                        System.Threading.Thread.Sleep(10);
                        if (floatHoehe <= 0)
                        {
                            setSpaceship(0, mondlandungsArgs.Schub);
                        }
                        else
                            setSpaceship(Convert.ToInt32(floatHoehe), mondlandungsArgs.Schub);
                    }
                finalHeight = mondlandungsArgs.Raumschiff.Hoehe;


                //Raumschiff ist gelandet
                if (finalHeight <= 0)
                {
                    if ((mondlandungsArgs.Raumschiff.Geschwindigkeit + 10) < 0)
                    {
                        bmpSpaceship = bmpSpaceshipBroken;
                        setSpaceship(0, 0);
                    }
                    else
                    {
                        //wegsehen: ugly code!
                        //Raumfahrer-Animation
                        setSpaceship(0, 0);
                        System.Threading.Thread.Sleep(500);
                        bmpSpaceship = bmpSpaceshipEmpty;
                        setSpaceship(0, 0);
                        System.Threading.Thread.Sleep(500);
                        setSpaceman(100);
                        System.Threading.Thread.Sleep(100);
                        setSpaceman(110);
                        System.Threading.Thread.Sleep(100);
                        setSpaceman(120);
                        System.Threading.Thread.Sleep(100);
                        setSpaceman(130);
                        System.Threading.Thread.Sleep(100);
                        setSpaceman(140);
                        System.Threading.Thread.Sleep(100);
                        setSpaceman(150);
                        System.Threading.Thread.Sleep(100);
                        setSpaceman(160);
                        System.Threading.Thread.Sleep(100);
                        setSpaceman(170);
                        System.Threading.Thread.Sleep(100);
                        gBuffer.DrawImage(bmpFlag, 200, 560, 37, 75);
                        pnl_Animation.CreateGraphics().DrawImageUnscaled(_backBuffer, 0, 0);
                        //end of ugly code!
                    }
                }
            }
             
        }

        /// <summary>
        /// Playing a moon landing with the selected population list of chromosomes
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void btn_Abspielen_Click(object sender, EventArgs e)
        {
            if (dgv_Population.SelectedRows.Count > 0)
            {
                btn_Play.Enabled = false;
                Cursor = Cursors.WaitCursor;
                bmpSpaceship = bmpSpaceshipIntact;
                finalHeight = Convert.ToInt32(txt_Height.Text);
                MoonSim.SimulationTurn += OnMondlandungsSimulationTurn;
                (dgv_Population.SelectedRows[0].Tag as Chromosome<IntGene>).computeFitness(MoonSim);
                MoonSim.SimulationTurn -= OnMondlandungsSimulationTurn;
                Cursor = Cursors.Default;
                btn_Play.Enabled = true;
            }
        }

        /// <summary>
        /// Abort simulation running
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void btn_SimAbbrechen_Click(object sender, EventArgs e)
        {
            simulationAbort = true;
        }

        /// <summary>
        /// Weight of the space traveler should include / remove the spacecraft weight
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void gewichtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
        }

        /// <summary>
        /// Cancel any simulation is running when you exit the program.
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Object</param>
        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            simulationAbort = true;
            closingApplication = true;
        }
    }

}

 