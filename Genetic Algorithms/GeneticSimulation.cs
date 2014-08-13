/*  Copyright (c) 2009 Daniel Baulig
 *
 *  This file is part of the Genetic Algorithms library.
 *
 *  The Genetic Algorithms library is free software: you can redistribute 
 *  it and/or modify it under the terms of the GNU General Public License 
 *  as published by the Free Software Foundation, either version 3 of the 
 *  License, or (at your option) any later version.
 *
 *  The Genetic Algorithms library is distributed in the hope that it will 
 *  be useful, but WITHOUT ANY WARRANTY; without even the implied warranty 
 *  of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with the Genetic Algorithms library.  If not, see 
 *  <http://www.gnu.org/licenses/>.
 * 
 *  e-mail: daniel dot baulig at gmx dot de
 *  
 *  If you wish to donate, please have a look at my Amazon Wishlist:
 *  http://www.amazon.de/wishlist/1GWSB78PYVFBQ
 */
using System;
using System.Collections;


namespace GeneticAlgorithms
{
    public delegate void OnSimulationTurn(object sender, EventArgs e);

    /// <summary>
    /// An instance of a Genetic Simulation
    /// </summary>
    /// <typeparam name="Gene">The type of the Gene.</typeparam>
    public class GeneticSimulation<Gene> where Gene: IGene, new()
    {
        // TODO underscore and comment
        private IFitnessFunctionProvider fitnessComputer;
        private IRecombinationProvider recombinator;
        private ISelectionProvider maleSelector;
        private ISelectionProvider femaleSelector;
        private double geneMutationRate;
        private double geneDropRate;
        private double geneDuplicationRate;
        private float totalFitness;
        private float averageChromosomeLength;
        private int populationSize;
        private int defaultChromosomeLength;
        private Chromosome<Gene> mostSuccessfulIndividual;
        private Chromosome<Gene> leastSuccessfulIndividual;
        protected Random randomizer;
        private bool abort = false;

        /// <summary>
        /// The population of Chromosomes
        /// TODO to list
        /// </summary>
        protected ArrayList population;

        /// <summary>
        /// Selects the male chromosome from the population
        /// </summary>
        /// <returns></returns>
        protected Chromosome<Gene> selectMaleChromosome()
        {
            return this.maleSelector.select(this.population, this.totalFitness) as Chromosome<Gene>;
        }

        /// <summary>
        /// Selects the female chromosome from the population
        /// </summary>
        /// <returns></returns>
        protected Chromosome<Gene> selectFemaleChromosome()
        {
            return this.femaleSelector.select(this.population, this.totalFitness) as Chromosome<Gene>;
        }

        /// <summary>
        /// Gets the population
        /// </summary>
        public ArrayList Population
        {
            get
            {
                return population;
            }
        }

        /// <summary>
        /// Gets the gene at a specified location
        /// </summary>
        /// <param name="index">The chromosome location to access</param>
        /// <returns>The chromosome</returns>
        public Chromosome<Gene> this[int index]
        {
            get
            {
                return population[index] as Chromosome<Gene>;
            }
        }

        /// <summary>
        /// Aborts the simulation.
        /// </summary>
        public void AbortSimulation()
        {
            abort = true;
        }

        /// <summary>
        /// Gets the size of the population.
        /// </summary>
        /// <value>
        /// The size of the population.
        /// </value>
        public int PopulationSize
        {
            get
            {
                return this.populationSize;
            }
        }

        /// <summary>
        /// Gets the default gene count.
        /// TODO why is this ever needed?
        /// </summary>
        /// <value>
        /// The default gene count.
        /// </value>
        public int DefaultGeneCount
        {
            get
            {
                return this.defaultChromosomeLength;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSimulation{Gene}"/> class.
        /// </summary>
        /// <param name="populationSize">Size of the population.</param>
        /// <param name="defaultGeneCount">The default gene count.</param>
        /// <param name="fitnessComputer">The fitness computer.</param>
        /// <param name="recombinator">The recombinator.</param>
        /// <param name="selector">The selector.</param>
        public GeneticSimulation(int populationSize, int defaultGeneCount,IFitnessFunctionProvider fitnessComputer, IRecombinationProvider recombinator, ISelectionProvider selector):
            this(populationSize, defaultGeneCount, fitnessComputer, recombinator, selector, selector)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSimulation{Gene}"/> class.
        /// </summary>
        /// <param name="populationSize">Size of the population.</param>
        /// <param name="defaultGeneCount">The default gene count.</param>
        /// <param name="fitnessComputer">The fitness computer.</param>
        /// <param name="recombinator">The recombinator.</param>
        /// <param name="maleSelector">The male selector.</param>
        /// <param name="femaleSelector">The female selector.</param>
        public GeneticSimulation(int populationSize, int defaultGeneCount, IFitnessFunctionProvider fitnessComputer, IRecombinationProvider recombinator, ISelectionProvider maleSelector, ISelectionProvider femaleSelector)
        {
            // Assign parameters to local variables
            this.fitnessComputer = fitnessComputer;
            this.recombinator = recombinator;
            this.maleSelector = maleSelector;
            this.femaleSelector = femaleSelector;
            this.populationSize = populationSize;
            this.defaultChromosomeLength = defaultGeneCount;

            // Initialise gene properties
            // TODO Constants
            this.geneMutationRate = 0.01d;
            this.geneDuplicationRate = 0.0d;
            this.geneDropRate = 0.0d;

            // Initialise the random number generation
            // TODO static member
            // TODO seed
            this.randomizer = new Random();

            // Initialise the simulation
            this.ResetSimulation();
        }

        /// <summary>
        /// Gets the average length of the chromosome.
        /// </summary>
        /// <value>
        /// The average length of the chromosome.
        /// </value>
        public float AverageChromosomeLength
        {
            get
            {
                return this.averageChromosomeLength;
            }
        }

        /// <summary>
        /// Gets the most successful individual.
        /// </summary>
        /// <value>
        /// The most successful individual.
        /// </value>
        public Chromosome<Gene> MostSuccessfulIndividual
        {
            get
            {
                return mostSuccessfulIndividual;
            }
        }

        /// <summary>
        /// Gets the least successful individual.
        /// </summary>
        /// <value>
        /// The least successful individual.
        /// </value>
        public Chromosome<Gene> LeastSuccessfulIndividual
        {
            get 
            {
                return leastSuccessfulIndividual;
            }
        }

        /// <summary>
        /// Gets the total fitness.
        /// </summary>
        /// <value>
        /// The total fitness.
        /// </value>
        public float TotalFitness
        {
            get
            {
                return totalFitness;
            }
        }

        /// <summary>
        /// Gets the average fitness.
        /// </summary>
        /// <value>
        /// The average fitness.
        /// </value>
        public float AverageFitness
        {
            get
            {
                return totalFitness / population.Count;
            }
        }

        /// <summary>
        /// Gets or sets the gene mutation rate.
        /// </summary>
        /// <value>
        /// The gene mutation rate.
        /// </value>
        public double GeneMutationRate
        {
            get { return geneMutationRate; }
            set { geneMutationRate = value; }
        }

        /// <summary>
        /// Gets or sets the gene duplication rate.
        /// </summary>
        /// <value>
        /// The gene duplication rate.
        /// </value>
        public double GeneDuplicationRate
        {
            get { return geneDuplicationRate; }
            set { geneDuplicationRate = value; }
        }

        /// <summary>
        /// Gets or sets the gene drop rate.
        /// </summary>
        /// <value>
        /// The gene drop rate.
        /// </value>
        public double GeneDropRate
        {
            get { return geneDropRate; }
            set { geneDropRate = value; }
        }

        /// <summary>
        /// Gets or sets the fitness computer.
        /// </summary>
        /// <value>
        /// The fitness computer.
        /// </value>
        public IFitnessFunctionProvider FitnessComputer
        {
            get 
            { 
                return fitnessComputer; 
            }
            set
            {
                fitnessComputer = value;
            }
        }

        /// <summary>
        /// Gets or sets the recombinator.
        /// </summary>
        /// <value>
        /// The recombinator.
        /// </value>
        public IRecombinationProvider Recombinator
        {
            get 
            { 
                return recombinator; 
            }
            set
            {
                recombinator = value;
            }
        }

        /// <summary>
        /// An event to be triggered after each iteration of the simulation
        /// </summary>
        public OnSimulationTurn SimulationTurn;

        /// <summary>
        /// Initialises the simulation
        /// </summary>
        virtual public void ResetSimulation()
        {
            // Reset the population
            population = new ArrayList(this.populationSize);

            // Fill the population with individuals
            while (this.population.Count < this.populationSize)
            {
                Chromosome<Gene> chromosome = new Chromosome<Gene>(this.defaultChromosomeLength);
                chromosome.computeFitness(this.fitnessComputer);
                this.population.Add(chromosome);
            }

            // Calculate statistics for the new generation
            populateStatistics();
        }

        /// <summary>
        /// Calculates the statistics (eg fitness) for each chromosome
        /// </summary>
        virtual protected void populateStatistics()
        {
            this.totalFitness = 0;
            this.averageChromosomeLength = 0;
            this.leastSuccessfulIndividual = null;
            this.mostSuccessfulIndividual = null;
            foreach (Chromosome<Gene> chromosome in this.population)
            {
                if (this.leastSuccessfulIndividual == null || this.leastSuccessfulIndividual.Fitness > chromosome.Fitness)
                    this.leastSuccessfulIndividual = chromosome;
                if (this.mostSuccessfulIndividual == null || this.mostSuccessfulIndividual.Fitness < chromosome.Fitness)
                    this.mostSuccessfulIndividual = chromosome;
                totalFitness += chromosome.Fitness;
                averageChromosomeLength += chromosome.GeneCount;
            }
            this.averageChromosomeLength /= this.population.Count;
        }

        /// <summary>
        /// Runs a single iteration of the simulation by creating a new population and calculating their fitness
        /// </summary>
        virtual public void RunSimulation()
        {
            ArrayList newPopulation = new ArrayList(populationSize);
            Chromosome<Gene> newChromosome = null;
            while (newPopulation.Count < this.populationSize)
            {
                try
                {
                    newChromosome = this.selectMaleChromosome().Recombine(this.selectFemaleChromosome(), this.recombinator);
                    for (int i = 0; i < newChromosome.GeneCount; i++)
                    {
                        if (this.randomizer.NextDouble() < this.geneMutationRate)
                            newChromosome[i].Mutate();
                        if (this.randomizer.NextDouble() < this.geneDuplicationRate)
                            newChromosome.DuplicateGene(i);
                        if (newChromosome.GeneCount > 1 && this.randomizer.NextDouble() < this.geneDropRate)
                            newChromosome.DropGene(i);
                    }
                    newChromosome.computeFitness(this.fitnessComputer);
                    newPopulation.Add(newChromosome);
                }
                catch (GenesIncompatibleException ignore)
                {
                    // TODO do something
                }
            }
                
            this.population = newPopulation;

            this.populateStatistics();

            if (SimulationTurn != null)
                SimulationTurn(this, EventArgs.Empty);
        }

        /// <summary>
        /// Runs multiple iterations of the simulation
        /// </summary>
        /// <param name="turns"></param>
        public void RunSimulation(int turns)
        {
            for (int i = 0; i < turns; i++)
            {
                this.RunSimulation();
                if (abort)
                {
                    abort = false;
                    break;
                }
            }
        }
    }
}
