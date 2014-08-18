
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
    /// Calculates the fitness of a bool gene by summing fitness and scaling
    /// I have no idea what exactly this scaling is used
    /// </summary>
    public class BoolGrowthLimitedFitness : IFitnessFunctionProvider<BoolGene>
    {
        /// <summary>
        /// See interface documentation
        /// </summary>
        public float ComputeFitness(List<BoolGene> genes)
        {
            // Sum all genes
            int sum = genes.Sum(x => Convert.ToInt32(x));

            // Return the scaled sum
            return sum * 10 - genes.Count*genes.Count;
        }
    }
}
