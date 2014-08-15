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

namespace GeneticAlgorithms
{
    /// <summary>
    /// Defines a single element of genetic data
    /// Derived classes must offer deep copying as per IClonable
    /// </summary>
    public interface IGene : ICloneable
    {
        /// <summary>
        /// Assigns the gene a random value
        /// </summary>
        void Mutate();

        /// <summary>
        /// Makes a value comparison of two seperate genes
        /// </summary>
        /// <param name="gene">The gene to compare to</param>
        /// <returns>True if the genes are equal</returns>
        bool Equals(object gene);
    }
}
