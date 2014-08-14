using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithms;
using GeneticAlgorithms.ExampleClasses;

namespace GeneticAlgorithmsExample_BoolStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneticSimulation<BoolGene> geneticSimulation = new GeneticSimulation<BoolGene>(
                5,
                10,
                new BoolSumFitness(),
                new CrossoverRecombinator(),
                new AlphaSelector());

            Console.WindowHeight = 50;
            Console.WindowWidth = 120;

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("Population {0}", i);

                foreach(Chromosome<BoolGene> chromosome in geneticSimulation.Population)
                {
                    foreach(BoolGene gene in chromosome.Genes)
                    {
                        Console.ForegroundColor = gene.Value ? ConsoleColor.Yellow : ConsoleColor.Red;
                        Console.BackgroundColor = gene.Value ? ConsoleColor.Red : ConsoleColor.Yellow;
                        Console.Write(gene.Value ? "1" : "0");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(Environment.NewLine);
                }
                Console.WriteLine();
                geneticSimulation.RunSimulation();
            }
            Console.ReadLine();
        }
    }
}
