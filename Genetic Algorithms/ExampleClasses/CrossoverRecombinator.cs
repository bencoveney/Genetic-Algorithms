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

namespace GeneticAlgorithms.ExampleClasses
{
    /// <summary>
    /// Produces a single gene list by recombining two parents at their mid-point
    /// </summary>
    public class CrossoverRecombinator : IRecombinationProvider
    {
        /// <summary>
        /// Recombines two parent gene lists into one child
        /// </summary>
        /// <typeparam name="Gene">The gene's type</typeparam>
        /// <param name="maleGenes">The father's genetic data</param>
        /// <param name="femaleGenes">The mother's genetic data</param>
        /// <returns>A child's genetic data</returns>
        public List<Gene> Recombine<Gene>(List<Gene> maleGenes, List<Gene> femaleGenes) where Gene: IGene, new()
        {
            // Check the genes are the correct length
            if (maleGenes.Count != femaleGenes.Count)
                throw new GenesIncompatibleException();

            // Caclulate the mid-point
            int middle = maleGenes.Count / 2;

            // Copy over the genes into a child
            List<Gene> child = new List<Gene>();
            maleGenes.GetRange(0, middle).ForEach(x => child.Add((Gene)x.Clone()));
            femaleGenes.GetRange(middle, femaleGenes.Count - middle).ForEach(x => child.Add((Gene)x.Clone()));

            return child;
        }
    }
}
