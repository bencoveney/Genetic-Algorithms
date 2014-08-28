using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithms;
using GeneticAlgorithms.ExampleClasses;

namespace GeneticAlgorithmsExample_BoolStrings
{
    class Program
    {
        /// <summary>
        /// The number of genes in each chromosome
        /// </summary>
        const int NUMBER_OF_GENES = 10;

        /// <summary>
        /// The number of chromosomes in each population
        /// </summary>
        const int POPULATION_SIZE = 5;

        /// <summary>
        /// An genetic simulation instance
        /// </summary>
        static GeneticSimulation<BoolGene> geneticSimulation = new GeneticSimulation<BoolGene>(
                POPULATION_SIZE,
                NUMBER_OF_GENES,
                new BoolSumFitness(),
                new CrossoverRecombinator(),
                new AlphaSelector());

        /// <summary>
        /// A count of how many populations have been created
        /// </summary>
        static int populationCounter = 0;

        /// <summary>
        /// A timer to track time taken to search
        /// </summary>
        static Stopwatch stopWatch;

        static void Main(string[] args)
        {
            // Set the console large enough to be able to view all the genes in the population
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = (NUMBER_OF_GENES + 1) * POPULATION_SIZE + 1;

            // Start the timer
            stopWatch = Stopwatch.StartNew();

            // Display the initial population
            WritePopulationToConsole();

            // Loop the simulation until an optimal value is found
            while (geneticSimulation.CurrentPopulation.BestChromosome.Fitness < NUMBER_OF_GENES)
            {
                // Generate the next population
                geneticSimulation.RunSimulation();

                // Output
                WritePopulationToConsole();
            }

            // Write some stats
            Console.Write("Number of populations: {0} | Time taken (ms): {1}", populationCounter, stopWatch.ElapsedMilliseconds);

            // Don't quit the program until the user has had the opportunity to see the results
            Console.ReadLine();
        }

        static void WritePopulationToConsole()
        {
            // Write the population name
            Console.WriteLine("Population {0}", populationCounter++);

            // Write some population statistics
            Console.WriteLine("Average Fitness: {0} | Max Fitness: {1}", geneticSimulation.CurrentPopulation.AverageFitness, geneticSimulation.CurrentPopulation.BestChromosome.Fitness);

            // Write each chromosome
            foreach (Chromosome<BoolGene> chromosome in geneticSimulation.CurrentPopulation.Chromosomes)
            {
                // Write each gene in each chromosome
                foreach (BoolGene gene in chromosome.Genes)
                {
                    Console.ForegroundColor = gene.Value ? ConsoleColor.Yellow : ConsoleColor.Red;
                    Console.BackgroundColor = gene.Value ? ConsoleColor.Red : ConsoleColor.Yellow;
                    Console.Write(gene.Value ? "1" : "0");
                }

                // Leave a gap between chromosomes
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
            // Leave a gap between populations
            Console.WriteLine(Environment.NewLine);
        }
    }
}
