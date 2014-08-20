using GeneticAlgorithms;
using GeneticAlgorithms.ExampleClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorithmsUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // borrowed from GA example boolstring
            GeneticSimulation<BoolGene> geneticSimulation = new GeneticSimulation<BoolGene>(
                10,
                10,
                new BoolSumFitness(),
                new CrossoverRecombinator(),
                new AlphaSelector());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GeneticAlgorithmsUI<BoolGene>(geneticSimulation));
        }
    }
}
