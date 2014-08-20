using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithms
{
    public class Population<Gene> 
        where Gene: IGene, new()

    {
        private List<Chromosome<Gene>> _population;

        public Population()
        {
            this._population = new List<Chromosome<Gene>>();
        }

        public Population(int populationSize, int chromosomeLength)
            : this()
        {
            for (int i = 0; i < populationSize; i++)
                this._population.Add(new Chromosome<Gene>(chromosomeLength));
        }

        public Population(List<Chromosome<Gene>> chromosomes)
        {
            this._population = chromosomes;
        }

        public List<Chromosome<Gene>> Chromosomes
        {
            get
            {
                return _population;
            }
        }

        public Chromosome<Gene> this[int index]
        {
            get
            { 
                return _population[index]; 
            }
        }

        public int Count
        {
            get
            {
                return _population.Count;
            }
        }

        public Chromosome<Gene> BestChromosome
        {
            get
            {
                return _population.OrderByDescending(x => x.Fitness).First();
            }
        }

        public Chromosome<Gene> WorstChromosome
        {
            get
            {
                return _population.OrderByDescending(x => x.Fitness).Last();
            }
        }

        public double AverageFitness
        {
            get
            {
                return TotalFitness / Count;
            }
        }

        public double TotalFitness
        { 
            get
            {
                return _population.Sum(x => x.Fitness);
            }
        }

        public double AverageChromosomeLength
        {
            get
            {
                return _population.Average(x => x.GeneCount);
            }
        }

        public Population<Gene> getChildGeneration()
        {
            throw new NotImplementedException();
        }
    }
}
