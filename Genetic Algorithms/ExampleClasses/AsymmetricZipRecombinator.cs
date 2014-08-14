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
using System.Collections.Generic;

namespace GeneticAlgorithms.ExampleClasses
{
    public class AsymmetricZipRecombinator : IRecombinationProvider
    {
        #region IRecombinationProvider Member

        public List<Gene> Recombine<Gene>(List<Gene> maleGenes, List<Gene> femaleGenes) where Gene: IGene, new()
        {
            List<Gene> child = new List<Gene>();

            if (maleGenes.Count < femaleGenes.Count)
            {
                child.CopyTo(femaleGenes.ToArray());
                for (int i = 0; i < maleGenes.Count; i++)
                {
                    if (!Convert.ToBoolean(i % 2))
                        child[i] = maleGenes[i];
                }
            }
            else
            {
                child.CopyTo(maleGenes.ToArray());
                for (int i = 0; i < femaleGenes.Count; i++)
                {
                    if (!Convert.ToBoolean(i % 2))
                        child[i] = femaleGenes[i];
                }
            }
            return child;
        }

        #endregion
    }
}
