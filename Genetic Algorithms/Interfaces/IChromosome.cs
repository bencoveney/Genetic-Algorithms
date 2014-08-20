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

using System.Collections.Generic;
namespace GeneticAlgorithms
{
    /// <summary>
    /// Defines a single individual's collection of genetic data
    /// </summary>
    public interface IChromosome
    {
        /// <summary>
        /// Provides the number of genes in the Chromosome
        /// </summary>
        int GeneCount
        {
            get;
        }

        /// <summary>
        /// Provides the fitness score of the individual
        /// </summary>
        float Fitness
        { 
            get;
        }

        /// <summary>
        /// Duplicates the gene at the specified location
        /// </summary>
        /// <param name="index">The location to duplicate the gene at</param>
        void DuplicateGene(int index);

        /// <summary>
        /// Removes the gene at the specified location
        /// </summary>
        /// <param name="index">The location to remove the gene from</param>
        void DropGene(int index);
    }
}
