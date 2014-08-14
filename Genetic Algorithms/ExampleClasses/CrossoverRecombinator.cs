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
    public class CrossoverRecombinator : 
        IRecombinationProvider
    {
        #region IRecombinationProvider Member

        public List<Gene> Recombine<Gene>(List<Gene> maleGenes, List<Gene> femaleGenes) where Gene: IGene, new()
        {
            if (maleGenes.Count != femaleGenes.Count)
                throw new GenesIncompatibleException();
            List<Gene> child = new List<Gene>(maleGenes.Count);

            // Caclulate the mid-point
            // TODO random mid-point
            int middle = maleGenes.Count / 2;

            // Copy over the genes
            child.CopyTo(maleGenes.GetRange(0, middle).ToArray());
            child.CopyTo(femaleGenes.GetRange(middle, femaleGenes.Count - middle).ToArray());

            return child;
        }

        #endregion
    }
}
