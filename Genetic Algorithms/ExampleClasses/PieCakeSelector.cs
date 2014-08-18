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

namespace GeneticAlgorithms.ExampleClasses
{
    /// <summary>
    /// Selects a random chromosome from the population with their chance of selection weighted by fitness
    /// </summary>
    public class PieCakeSelector : ISelectionProvider
    {
        /// <summary>
        /// Random number provider
        /// </summary>
        private static Random randomizer = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// See interface documentation
        /// </summary>
        public Chromosome Select<Chromosome>(List<Chromosome> population, float totalFitness) where Chromosome: IChromosome
        {
            // Calculate how far round the pie to select
            double selectionPoint = PieCakeSelector.randomizer.NextDouble() * totalFitness;

            int index = 0;
            try
            {
                // While there is still distance round the pie to travel
                while (selectionPoint > (population[index] as IChromosome).Fitness)
                    // move to the next chromosome 
                    // this is done by skipping the current one and subtracting it's fitness from the distance to travel
                    selectionPoint -= (population[index++] as IChromosome).Fitness;
            }
            catch (ArgumentOutOfRangeException)
            {   
                // Can possibly occur due to numerical effects, when last chromosome selected
                index--;
            }

            return population[index];
        }
    }
}
