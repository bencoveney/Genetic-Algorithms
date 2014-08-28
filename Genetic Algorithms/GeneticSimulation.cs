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
        #region Constants

        private const double DEFAULT_MUTATION_RATE = 0.05;
        private const double DEFAULT_DUPLICATION_RATE = 0.0;
        private const double DEFAULT_DROP_RATE = 0.0;

        #endregion

        /// <summary>
        /// A random number provider
        /// </summary>
        protected static Random Randomizer = new Random(DateTime.Now.Millisecond);
        
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
        private ISelectionProvider _selector;

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
        /// </summary>
        protected List<Population<Gene>> _populations;

        /// <summary>
        /// An event to be triggered after each iteration of the simulation
        /// </summary>
        public OnSimulationTurn SimulationTurn;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSimulation{Gene}"/> class.
        /// </summary>
        /// <param name="populationSize">Size of the population.</param>
        /// <param name="defaultGeneCount">The default gene count.</param>
        /// <param name="fitnessComputer">The fitness computer.</param>
        /// <param name="recombinator">The recombinator.</param>
        /// <param name="selector">The selector.</param>
        public GeneticSimulation(int populationSize, int defaultGeneCount, IFitnessFunctionProvider<Gene> fitnessComputer, IRecombinationProvider recombinator, ISelectionProvider selector)
        {
            // Assign parameters to local variables
            this._fitnessCalculator = fitnessComputer;
            this._recombinator = recombinator;
            this._selector = selector;
            this._populationSize = populationSize;
            this._defaultChromosomeLength = defaultGeneCount;

            // Initialise local variables
            this._populations = new List<Population<Gene>>();

            // Initialise gene properties
            // TODO Constants
            this._geneMutationRate = 0.05d;
            this._geneDuplicationRate = 0.0d;
            this._geneDropRate = 0.0d;

            // Initialise the simulation
            this.ResetSimulation();
        }

        /// <summary>
        /// Gets the most recently generated population
        /// </summary>
        public Population<Gene> CurrentPopulation
        {
            get
            {
                return _populations.Last();
            }
        }

        /// <summary>
        /// Gets all populations which have been generated by this simulation
        /// </summary>
        public List<Population<Gene>> Populations
        {
            get
            {
                return _populations;
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
                return _populations[index] as Chromosome<Gene>;
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

            _populations.Add(new Population<Gene>(this._populationSize, _defaultChromosomeLength));

            // Fill the population with individuals
            while (this.CurrentPopulation.Chromosomes.Count < this._populationSize)
            {
                Chromosome<Gene> chromosome = new Chromosome<Gene>(this._defaultChromosomeLength);
                chromosome.computeFitness(this._fitnessCalculator);
                this.CurrentPopulation.Chromosomes.Add(chromosome);
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
                    PairedChromosomes<Gene> couple = _selector.SelectCouple<Gene>(CurrentPopulation);
                    PairedChromosomes<Gene> children = couple.Reproduce(_recombinator);
                    foreach (Chromosome<Gene> child in children.Chromosomes)
                    {
                        // For each gene in the chromosome
                        for (int i = 0; i < child.GeneCount; i++)
                        {
                            // Mutate genes
                            if (Randomizer.NextDouble() < this._geneMutationRate)
                                child[i].Mutate();

                            // Duplicate genes
                            if (Randomizer.NextDouble() < this._geneDuplicationRate)
                                child.DuplicateGene(i);

                            // Drop genes
                            if (child.GeneCount > 1 && Randomizer.NextDouble() < this._geneDropRate)
                                child.DropGene(i);
                        }
                        child.computeFitness(this._fitnessCalculator);
                        newPopulation.Add(child);
                    }
                }

                // Handle incompatible genes
                catch (GenesIncompatibleException)
                {
                    // TODO do something
                }
            }

            this._populations.Add(new Population<Gene>(newPopulation));

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
    }
}
