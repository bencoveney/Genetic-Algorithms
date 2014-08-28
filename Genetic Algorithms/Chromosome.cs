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
using System.Text;

namespace GeneticAlgorithms
{
    /// <summary>
    /// A Generic instance of a chromosome; Contains a strand of genes and the methods through which to act on them
    /// </summary>
    /// <typeparam name="Gene">The gene's Type.</typeparam>
    public class Chromosome<Gene>: IChromosome, IComparable<Chromosome<Gene>> where Gene: IGene, new()
    {
        /// <summary>
        /// A list of genes. It is assumed genes are stored by type Gene
        /// </summary>
        protected List<Gene> _genes;

        /// <summary>
        /// The fitness of this chromosome
        /// </summary>
        private float _fitness;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chromosome{Gene}"/> class with randomly initialised genes.
        /// </summary>
        /// <param name="geneCount">The number of genes to create within the chromosome</param>
        public Chromosome(int geneCount)
        {
            // Initialise the gene list
            this._genes = new List<Gene>(geneCount);

            // Populate it with new genes
            while (this._genes.Count < geneCount)
                this._genes.Add(new Gene());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chromosome{Gene}"/> class.
        /// </summary>
        /// <param name="genes">The genes.</param>
        /// <exception cref="System.ArrayTypeMismatchException"></exception>
        public Chromosome(List<Gene> genes)
        {
            // If no genes were passed all we need to do is initialise the local variable 
            if (genes.Count <= 0)
            {
                this._genes = new List<Gene>();
                return;
            }

            // Check the genes passed in are of the correct type
            if (genes[genes.Count - 1].GetType() != typeof(Gene))
                throw new ArrayTypeMismatchException();

            // Assign the passed genes to the local variable
            this._genes = new List<Gene>(genes);
        }

        /// <summary>
        /// Accessor for _fitness
        /// </summary>
        /// <value>
        /// The fitness.
        /// </value>
        public float Fitness
        {
            get
            {
                return this._fitness;
            }
        }

        /// <summary>
        /// Gets the length of the chromosome
        /// </summary>
        /// <value>
        /// The gene count.
        /// </value>
        public int GeneCount
        {
            get
            {
                return this._genes.Count;
            }
        }

        /// <summary>
        /// Gets the genes.
        /// </summary>
        /// <value>
        /// The genes.
        /// </value>
        public List<Gene> Genes
        {
            get
            {
                return this._genes;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Gene"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Gene"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public Gene this[int index]
        {
            get
            {
                return (Gene)_genes[index];
            }
            set
            {
                _genes[index] = value;
            }
        }

        /// <summary>
        /// Compares the current object with another object of the same type to facilitate sorting of chromosomes
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public int CompareTo(Chromosome<Gene> other)
        {
            return this._fitness.CompareTo(other._fitness);
        }

        /// <summary>
        /// Calculates the chromosome's fitness
        /// </summary>
        /// <param name="provider">The fitness function provider to use in the calculation</param>
        public void computeFitness(IFitnessFunctionProvider<Gene> provider)
        {
            this._fitness = provider.ComputeFitness(this._genes);
        }

        /// <summary>
        /// Removes a gene from the specified index
        /// </summary>
        /// <param name="index">The index of the gene to remove</param>
        public void DropGene(int index)
        {
            this._genes.RemoveAt(index);
        }

        /// <summary>
        /// Duplicates a gene from the specified index
        /// </summary>
        /// <param name="index">The index of the gene to duplicate</param>
        public void DuplicateGene(int index)
        {
            Gene gene = new Gene();
            gene = (Gene) this[index].Clone();
            this._genes.Insert(index, gene);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            // Build a string by combining each gene
            StringBuilder result = new StringBuilder();
            Genes.ForEach(x => result.Append(x.ToString()));

            return result.ToString();
        }
    }
}
