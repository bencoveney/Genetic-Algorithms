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
        /// See interface documentation
        /// </summary>
        public PairedChromosomes<Gene> Recombine<Gene>(PairedChromosomes<Gene> couple) where Gene: IGene, new()
        {
            // Check the genes are the correct length
            if (couple.Male.GeneCount != couple.Female.GeneCount)
                throw new GenesIncompatibleException();

            // Caclulate the mid-point
            int middle = couple.Male.GeneCount / 2;

            // Copy over the genes into a child...
            List<Gene> maleChild = new List<Gene>();
            List<Gene> femaleChild = new List<Gene>();
            // ...Before crossover point
            couple.Male.Genes.GetRange(0, middle).ForEach(x => maleChild.Add((Gene)x.Clone()));
            couple.Female.Genes.GetRange(0, middle).ForEach(x => femaleChild.Add((Gene)x.Clone()));
            // ...After crossover point
            couple.Male.Genes.GetRange(middle, couple.Female.GeneCount - middle).ForEach(x => femaleChild.Add((Gene)x.Clone()));
            couple.Female.Genes.GetRange(middle, couple.Female.GeneCount - middle).ForEach(x => maleChild.Add((Gene)x.Clone()));

            return new PairedChromosomes<Gene>(
                new Chromosome<Gene>(maleChild),
                new Chromosome<Gene>(femaleChild));
        }
    }
}
