using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GeneticAlgorithms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GeneticAlgorithmsUI
{
    public partial class GeneticAlgorithmsUI<Gene> : Form where Gene: IGene, new()
    {
        /// <summary>
        /// The genetic simulation instance being used
        /// </summary>
        private GeneticSimulation<Gene> _geneticSimulation;

        /// <summary>
        /// The data represented by the graph and spreadsheet
        /// </summary>
        private BindingList<GenerationDataGridItem> _generationsCreated;

        /// <summary>
        /// A data series for the maximum fitness in each generation
        /// </summary>
        private Series _maximumFitness;

        /// <summary>
        /// A data series for the average fitness in each generation
        /// </summary>
        private Series _averageFitness;
        
        /// <summary>
        /// Creates a new instance of the Genetic Algorithms UI Class
        /// </summary>
        /// <param name="geneticSimulation">The genetic simulation to provide an interface for</param>
        public GeneticAlgorithmsUI(GeneticSimulation<Gene> geneticSimulation)
        {
            InitializeComponent();

            // Assign local data members
            _geneticSimulation = geneticSimulation;
            _generationsCreated = new BindingList<GenerationDataGridItem>();

            // Add the data storing method to be triggered each time the simulation runs
            _geneticSimulation.SimulationTurn += storeCurrentGeneration;

            // Update Data Views
            initialiseDataViews();

            // Store the initial data
            storeCurrentGeneration(new object(), new EventArgs());
        }

        /// <summary>
        /// Displays the settings dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void launchSettingsButton_Click(object sender, EventArgs e)
        {
            GeneticAlgorithmsSettingsDialog settingsForm = new GeneticAlgorithmsSettingsDialog();
            settingsForm.ShowDialog();
        }

        /// <summary>
        /// Resets the geneticSimulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetSimulationButton_Click(object sender, EventArgs e)
        {
            _geneticSimulation.ResetSimulation();
        }

        /// <summary>
        /// Runs a single iteration of the genetic simulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simulateSingleButton_Click(object sender, EventArgs e)
        {
            _geneticSimulation.RunSimulation();
        }

        /// <summary>
        /// Runs multiple iterations of the genetic simulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simulateMultipleButton_Click(object sender, EventArgs e)
        {
            _geneticSimulation.RunSimulation((int)numberOfGenerationsNumericUpDown.Value);
        }

        /// <summary>
        /// Adds the current generation's data to the datagridview and graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storeCurrentGeneration(object sender, EventArgs e)
        {
            // TODO everything about this method and chromosomeToString is screaming "Shit that should be refactored into GeneticAlgorithms project"

            // Build generation grid item
            GenerationDataGridItem record = new GenerationDataGridItem();
            record.GenerationName = string.Format("Generation Number {0}", _geneticSimulation.Populations.Count);
            record.AverageFitness = _geneticSimulation.CurrentPopulation.AverageFitness;
            record.MaximumFitness = _geneticSimulation.CurrentPopulation.BestChromosome.Fitness;
            record.MostSuccessfulChromosome = _geneticSimulation.CurrentPopulation.BestChromosome.ToString();
            record.LeastSuccessfulChromosome = _geneticSimulation.CurrentPopulation.WorstChromosome.ToString();

            // Assign it to the list
            _generationsCreated.Add(record);

            // Add the data to the graphs
            int generationNumber = _generationsCreated.Count - 1;
            _maximumFitness.Points.AddXY(generationNumber, record.MaximumFitness);
            _averageFitness.Points.AddXY(generationNumber, record.AverageFitness);
        }

        /// <summary>
        /// Initializes the datagrid and graphs
        /// </summary>
        private void initialiseDataViews()
        {
            // Assign the generations list to the datagridview
            simulationDataGridView.DataSource = _generationsCreated;

            // Chart area formatting
            ChartArea chartArea = fitnessGraph.ChartAreas.First();
            chartArea.AxisX.Title = "Generations";
            chartArea.AxisX.MajorGrid.LineColor = Color.DarkGray;
            //chartArea.AxisX.MinorGrid.Enabled = true;
            //chartArea.AxisX.MinorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.Title = "Fitness";
            chartArea.AxisY.MajorGrid.LineColor = Color.DarkGray;
            //chartArea.AxisY.MinorGrid.Enabled = true;
            //chartArea.AxisY.MinorGrid.LineColor = Color.LightGray;

            // Set up the fitness graph
            Title title = new Title("Fitness Per Generation");
            title.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            fitnessGraph.Titles.Add(title);

            // Create the graph line for the maximum fitness
            _maximumFitness = new Series("Maximum Fitness");
            _maximumFitness.ChartType = SeriesChartType.Line;
            _maximumFitness.BorderWidth = 3;
            fitnessGraph.Series.Add(_maximumFitness);

            // Create the graph line for the maximum fitness
            _averageFitness = new Series("Average Fitness");
            _averageFitness.ChartType = SeriesChartType.Line;
            _averageFitness.BorderWidth = 3;
            fitnessGraph.Series.Add(_averageFitness);
        }
    }
}
