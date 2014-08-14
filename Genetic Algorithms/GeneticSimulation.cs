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
using System.Collections.Generic;
using System.Linq;


namespace GeneticAlgorithms
{
    public delegate void OnSimulationTurn(object sender, EventArgs e);

    /// <summary>
    /// An instance of a Genetic Simulation
    /// </summary>
    /// <typeparam name="Gene">The type of the Gene.</typeparam>
    public class GeneticSimulation<Gene> where Gene: IGene, new()
    {
        #region Static Members

        /// <summary>
        /// A random number provider
        /// </summary>
        protected static Random Randomizer = new Random(DateTime.Now.Millisecond);
        
        #endregion

        #region Member Data

        /// <summary>
        /// The fitness function used to calculate fitness
        /// </summary>
        private IFitnessFunctionProvider<Gene> _fitnessCalculator;

        /// <summary>
        /// Used to combine two chromosomes into one
        /// </summary>
        private IRecombinationProvider _recombinator;

        /// <summary>
        /// The selection strategy used for picking the male chromosome
        /// </summary>
        private ISelectionProvider _maleSelector;

        /// <summary>
        /// The selection strategy used for picking the female chromosome
        /// </summary>
        private ISelectionProvider _femaleSelector;

        /// <summary>
        /// The rate at which mutation occurs in the chromosome
        /// </summary>
        private double _geneMutationRate;

        /// <summary>
        /// The rate at which genes are removed from the chromosome
        /// </summary>
        private double _geneDropRate;

        /// <summary>
        /// The rate at which genes are duplicated within the chromosome
        /// </summary>
        private double _geneDuplicationRate;

        /// <summary>
        /// The amount of chromosomes in the population
        /// </summary>
        private int _populationSize;

        /// <summary>
        /// The amount of genes in each chromosome by default
        /// </summary>
        private int _defaultChromosomeLength;

        /// <summary>
        /// Used to interrupt the running simulations
        /// </summary>
        private bool _abort = false;

        /// <summary>
        /// The population of Chromosomes
        /// TODO to list
        /// </summary>
        protected List<Chromosome<Gene>> _population;

        /// <summary>
        /// An event to be triggered after each iteration of the simulation
        /// </summary>
        public OnSimulationTurn SimulationTurn;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSimulation{Gene}"/> class.
        /// </summary>
        /// <param name="populationSize">Size of the population.</param>
        /// <param name="defaultGeneCount">The default gene count.</param>
        /// <param name="fitnessComputer">The fitness computer.</param>
        /// <param name="recombinator">The recombinator.</param>
        /// <param name="selector">The selector.</param>
        public GeneticSimulation(int populationSize, int defaultGeneCount, IFitnessFunctionProvider<Gene> fitnessComputer, IRecombinationProvider recombinator, ISelectionProvider selector) :
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
        public GeneticSimulation(int populationSize, int defaultGeneCount, IFitnessFunctionProvider<Gene> fitnessComputer, IRecombinationProvider recombinator, ISelectionProvider maleSelector, ISelectionProvider femaleSelector)
        {
            // Assign parameters to local variables
            this._fitnessCalculator = fitnessComputer;
            this._recombinator = recombinator;
            this._maleSelector = maleSelector;
            this._femaleSelector = femaleSelector;
            this._populationSize = populationSize;
            this._defaultChromosomeLength = defaultGeneCount;

            // Initialise gene properties
            // TODO Constants
            this._geneMutationRate = 0.01d;
            this._geneDuplicationRate = 0.0d;
            this._geneDropRate = 0.0d;

            // Initialise the simulation
            this.ResetSimulation();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the population
        /// </summary>
        public List<Chromosome<Gene>> Population
        {
            get
            {
                return _population;
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
                return _population[index] as Chromosome<Gene>;
            }
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
                return this._populationSize;
            }
        }

        /// <summary>
        /// Gets the default gene count.
        /// </summary>
        /// <value>
        /// The default gene count.
        /// </value>
        public int DefaultGeneCount
        {
            get
            {
                return this._defaultChromosomeLength;
            }
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
                return _population.Average(x => x.Fitness);
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
                return _population.OrderByDescending(x => x.Fitness).First();
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
                return _population.OrderByDescending(x => x.Fitness).Last();
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
                return _population.Sum(x => x.Fitness);
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
                return TotalFitness / _populationSize;
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
            get { return _geneMutationRate; }
            set { _geneMutationRate = value; }
        }

        /// <summary>
        /// Gets or sets the gene duplication rate.
        /// </summary>
        /// <value>
        /// The gene duplication rate.
        /// </value>
        public double GeneDuplicationRate
        {
            get { return _geneDuplicationRate; }
            set { _geneDuplicationRate = value; }
        }

        /// <summary>
        /// Gets or sets the gene drop rate.
        /// </summary>
        /// <value>
        /// The gene drop rate.
        /// </value>
        public double GeneDropRate
        {
            get { return _geneDropRate; }
            set { _geneDropRate = value; }
        }

        /// <summary>
        /// Gets or sets the fitness computer.
        /// </summary>
        /// <value>
        /// The fitness computer.
        /// </value>
        public IFitnessFunctionProvider<Gene> FitnessComputer
        {
            get
            {
                return _fitnessCalculator;
            }
            set
            {
                _fitnessCalculator = value;
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
                return _recombinator;
            }
            set
            {
                _recombinator = value;
            }
        }

        #endregion

        #region Member Methods

        /// <summary>
        /// Selects the male chromosome from the population
        /// </summary>
        /// <returns></returns>
        protected Chromosome<Gene> selectMaleChromosome()
        {
            return this._maleSelector.Select(this._population, this.TotalFitness);
        }

        /// <summary>
        /// Selects the female chromosome from the population
        /// </summary>
        /// <returns></returns>
        protected Chromosome<Gene> selectFemaleChromosome()
        {
            return this._femaleSelector.Select(this._population, this.TotalFitness);
        }

        /// <summary>
        /// Aborts the simulation.
        /// </summary>
        public void AbortSimulation()
        {
            _abort = true;
        }

        /// <summary>
        /// Initialises the simulation
        /// </summary>
        virtual public void ResetSimulation()
        {
            // Reset the population
            _population = new List<Chromosome<Gene>>(this._populationSize);

            // Fill the population with individuals
            while (this._population.Count < this._populationSize)
            {
                Chromosome<Gene> chromosome = new Chromosome<Gene>(this._defaultChromosomeLength);
                chromosome.computeFitness(this._fitnessCalculator);
                this._population.Add(chromosome);
            }
        }

        /// <summary>
        /// Runs a single iteration of the simulation by creating a new population and calculating their fitness
        /// </summary>
        virtual public void RunSimulation()
        {
            // Create and populate a new population
            List<Chromosome<Gene>> newPopulation = new List<Chromosome<Gene>>(_populationSize);
            while (newPopulation.Count < this._populationSize)
            {

                try
                {

                    // Combine the parents to make a child
                    Chromosome<Gene> newChromosome = this.selectMaleChromosome().Recombine(this.selectFemaleChromosome(), _recombinator);

                    // For each gene in the chromosome
                    for (int i = 0; i < newChromosome.GeneCount; i++)
                    {
                        
                        // Mutate genes
                        if (Randomizer.NextDouble() < this._geneMutationRate)
                            newChromosome[i].Mutate();

                        // Duplicate genes
                        if (Randomizer.NextDouble() < this._geneDuplicationRate)
                            newChromosome.DuplicateGene(i);

                        // Drop genes
                        if (newChromosome.GeneCount > 1 && Randomizer.NextDouble() < this._geneDropRate)
                            newChromosome.DropGene(i);
                    }
                    newChromosome.computeFitness(this._fitnessCalculator);
                    newPopulation.Add(newChromosome);
                }

                // Handle incompatible genes
                catch (GenesIncompatibleException ignore)
                {
                    // TODO do something
                }
            }

            this._population = newPopulation;

            if (SimulationTurn != null)
                SimulationTurn(this, EventArgs.Empty);
        }

        /// <summary>
        /// Runs multiple iterations of the simulation
        /// </summary>
        /// <param name="turns">The number of times to run the simulation</param>
        public void RunSimulation(int turns)
        {
            // for each turn
            for (int i = 0; i < turns; i++)
            {
                // Run the simulation
                this.RunSimulation();
                
                // If the simulation has been aborted, quit the simulation
                if (_abort)
                {
                    _abort = false;
                    break;
                }
            }
        }

        #endregion
    }
}
