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
using System.Drawing;

namespace GeneticAlgorithmsUI
{
    public partial class GeneticAlgorithmsUI<Gene> : Form where Gene: IGene, new()
    {
        private GeneticSimulation<Gene> _geneticSimulation;

        private BindingList<GenerationDataGridItem> _generationsCreated;

        private Series _maximumFitness;

        private Series _averageFitness;
            
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

        #region Button Events

        private void launchSettingsButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void resetSimulationButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void simulateSingleButton_Click(object sender, EventArgs e)
        {
            _geneticSimulation.RunSimulation();
        }

        private void simulateMultipleButton_Click(object sender, EventArgs e)
        {
            _geneticSimulation.RunSimulation((int)numberOfGenerationsNumericUpDown.Value);
        }

        #endregion

        private void storeCurrentGeneration(object sender, EventArgs e)
        {
            // TODO everything about this method and chromosomeToString is screaming "Shit that should be refactored into GeneticAlgorithms project"

            // Build generation grid item
            GenerationDataGridItem record = new GenerationDataGridItem();
            record.GenerationName = string.Format("Generation Number {0}", _geneticSimulation.GenerationsCreated);
            record.AverageFitness = _geneticSimulation.AverageFitness;
            record.MaximumFitness = _geneticSimulation.MostSuccessfulIndividual.Fitness;
            record.MostSuccessfulChromosome = chromosomeToString(_geneticSimulation.MostSuccessfulIndividual);
            record.LeastSuccessfulChromosome = chromosomeToString(_geneticSimulation.LeastSuccessfulIndividual);

            // Assign it to the list
            _generationsCreated.Add(record);

            // Add the data to the graphs
            int generationNumber = _generationsCreated.Count - 1;
            _maximumFitness.Points.AddXY(generationNumber, record.MaximumFitness);
            _averageFitness.Points.AddXY(generationNumber, record.AverageFitness);
        }

        // Should really just have a Chromosome<IGene>.ToString()
        private string chromosomeToString(Chromosome<Gene> Chromosome)
        {
            StringBuilder result = new StringBuilder();
            Chromosome.Genes.ForEach(x=>result.Append(x.ToString()));
            return result.ToString();
        }

        private void initialiseDataViews()
        {
            // Assign the generations list to the datagridview
            simulationDataGridView.DataSource = _generationsCreated;

            // Chart area formatting
            ChartArea chartArea = fitnessGraph.ChartAreas.First();
            chartArea.AxisX.Title = "Generations";
            chartArea.AxisX.MajorGrid.LineColor = Color.DarkGray;
            chartArea.AxisX.MinorGrid.Enabled = true;
            chartArea.AxisX.MinorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.Title = "Fitness";
            chartArea.AxisY.MajorGrid.LineColor = Color.DarkGray;
            chartArea.AxisY.MinorGrid.Enabled = true;
            chartArea.AxisY.MinorGrid.LineColor = Color.LightGray;

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
