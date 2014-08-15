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

namespace GeneticAlgorithms.ExampleClasses
{
    /// <summary>
    /// Produces a single gene list by recombining two parents at a random point
    /// </summary>
    public class AsymmetricCrossoverRecombinator : IRecombinationProvider
    {
        /// <summary>
        /// Random number generator
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);
        
        /// <summary>
        /// Recombines two parent gene lists into one child
        /// </summary>
        /// <typeparam name="Gene">The gene's type</typeparam>
        /// <param name="maleGenes">The father's genetic data</param>
        /// <param name="femaleGenes">The mother's genetic data</param>
        /// <returns>A child's genetic data</returns>
        public List<Gene> Recombine<Gene>(List<Gene> maleGenes, List<Gene> femaleGenes) where Gene : IGene, new()
        {
            // Calculate which parent has a longer gene
            List<Gene> longerGene = maleGenes.Count >= femaleGenes.Count ? maleGenes : femaleGenes;
            List<Gene> shorterGene = maleGenes.Count < femaleGenes.Count ? maleGenes : femaleGenes;

            // Find a random crossover point
            int crossoverPoint = random.Next(shorterGene.Count);

            // Build the child's genetic data
            List<Gene> child = new List<Gene>();
            for(int i = 0; i < longerGene.Count; i++)
            {
                // If before the crossover take from the shorter gene, otherwise take from the longer one
                Gene gene = i <= crossoverPoint ? shorterGene[i] : longerGene[i];
                child.Add(gene);
            }
            
            return child;
        }
    }
}
