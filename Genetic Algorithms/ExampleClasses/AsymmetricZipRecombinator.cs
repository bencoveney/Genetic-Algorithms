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
    /// <summary>
    /// Produces a single gene list by recombining two parents genes alternately
    /// </summary>
    public class AsymmetricZipRecombinator : IRecombinationProvider
    {
        /// <summary>
        /// See interface documentation
        /// </summary>
        public PairedChromosomes<Gene> Recombine<Gene>(PairedChromosomes<Gene> couple) where Gene : IGene, new()
        {
            // Calculate which parent has a longer gene
            List<Gene> longerGene = couple.Male.Genes.Count >= couple.Female.Genes.Count ? couple.Male.Genes : couple.Female.Genes;
            List<Gene> shorterGene = couple.Male.Genes.Count < couple.Female.Genes.Count ? couple.Male.Genes : couple.Female.Genes;

            // for each parent gene add a gene to the children
            List<Gene> maleChild = new List<Gene>();
            List<Gene> femaleChild = new List<Gene>();
            for (int i = 0; i < longerGene.Count; i++)
            {
                if (i < shorterGene.Count)
                {
                    // alternate which child takes from which parent
                    if (i % 2 == 0 || i >= shorterGene.Count)
                    {
                        maleChild.Add((Gene)longerGene[i].Clone());
                        femaleChild.Add((Gene)shorterGene[i].Clone());
                    } 
                    else
                    {
                        femaleChild.Add((Gene)longerGene[i].Clone());
                        maleChild.Add((Gene)shorterGene[i].Clone());
                    }
                }
                else
                {
                    // If theres nothing left on the shorter gene then only take from the longer one
                    maleChild.Add(longerGene[i]);
                }
            }

            return new PairedChromosomes<Gene>(
                new Chromosome<Gene>(maleChild),
                new Chromosome<Gene>(femaleChild));
        }
    }
}
