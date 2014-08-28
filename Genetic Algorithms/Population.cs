using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Defines a collection of chromosomes chich were generated together
    /// 
    /// </summary>
    /// <typeparam name="Gene"></typeparam>
    public class Population<Gene> 
        where Gene: IGene, new()
    {
        /// <summary>
        /// The collection of chromosomes
        /// </summary>
        private List<Chromosome<Gene>> _population;

        /// <summary>
        /// Initializes an empty population
        /// </summary>
        public Population()
        {
            // TODO protected?
            this._population = new List<Chromosome<Gene>>();
        }

        /// <summary>
        /// Initializes a population and fills it with newly generated chromosomes
        /// </summary>
        /// <param name="populationSize">The size of the population</param>
        /// <param name="chromosomeLength">The length of each chromosome</param>
        public Population(int populationSize, int chromosomeLength)
            : this()
        {
            // create each chromosome and add it to the population
            for (int i = 0; i < populationSize; i++)
                this._population.Add(new Chromosome<Gene>(chromosomeLength));
        }

        /// <summary>
        /// Creates a population object from exisiting chromosomes
        /// </summary>
        /// <param name="chromosomes"></param>
        public Population(List<Chromosome<Gene>> chromosomes)
        {
            this._population = chromosomes;
        }

        /// <summary>
        /// Gets the list of chromosomes
        /// </summary>
        public List<Chromosome<Gene>> Chromosomes
        {
            get
            {
                return _population;
            }
        }

        /// <summary>
        /// Gets the chromosome at the specified index in the population
        /// </summary>
        /// <param name="index">The index of the chromosome</param>
        /// <returns>The chromosome</returns>
        public Chromosome<Gene> this[int index]
        {
            get
            { 
                return _population[index]; 
            }
        }

        /// <summary>
        /// Gets the number of chromosomes currently in this population
        /// </summary>
        public int Count
        {
            get
            {
                return _population.Count;
            }
        }

        /// <summary>
        /// Gets the chromosome with the highest fitness rating
        /// </summary>
        public Chromosome<Gene> BestChromosome
        {
            get
            {
                return _population.OrderByDescending(x => x.Fitness).First();
            }
        }

        /// <summary>
        /// Gets the chromosome with the lowest fitness rating
        /// </summary>
        public Chromosome<Gene> WorstChromosome
        {
            get
            {
                return _population.OrderByDescending(x => x.Fitness).Last();
            }
        }

        /// <summary>
        /// Gets the average of each chromosome's fitness
        /// </summary>
        public double AverageFitness
        {
            get
            {
                return TotalFitness / Count;
            }
        }

        /// <summary>
        /// Gets the sum of the fitness of all chromosomes
        /// </summary>
        public double TotalFitness
        { 
            get
            {
                return _population.Sum(x => x.Fitness);
            }
        }

        /// <summary>
        /// Gets the average chromosome's length
        /// </summary>
        public double AverageChromosomeLength
        {
            get
            {
                return _population.Average(x => x.GeneCount);
            }
        }

        /// <summary>
        /// Creates a child generation by performing crossover and mutation operations on the current generation
        /// </summary>
        /// <returns></returns>
        public Population<Gene> getChildGeneration()
        {
            throw new NotImplementedException();
        }
    }
}
