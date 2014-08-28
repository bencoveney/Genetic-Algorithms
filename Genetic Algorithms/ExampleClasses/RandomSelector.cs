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
    /// Selects an individual from the population completely randomly
    /// </summary>
    public class RandomSelector : ISelectionProvider
    {
        /// <summary>
        /// Random number provider
        /// </summary>
        static protected Random randomizer = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// See interface documentation
        /// </summary>
        public PairedChromosomes<Gene> SelectCouple<Gene>(Population<Gene> population) where Gene : IGene, new()
        {
            // Select a couple randomly
            Chromosome<Gene> male = SelectSingle<Gene>(population);
            Chromosome<Gene> female = SelectSingle<Gene>(population);

            return new PairedChromosomes<Gene>(male, female);
        }

        /// <summary>
        /// See interface documentation
        /// </summary>
        public Chromosome<Gene> SelectSingle<Gene>(Population<Gene> population) where Gene : IGene, new()
        {
            // Selects an individual at random
            return population[RandomSelector.randomizer.Next(0, population.Count)];
        }
    }
}
