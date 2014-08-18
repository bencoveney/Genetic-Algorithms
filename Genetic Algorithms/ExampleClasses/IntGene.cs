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

namespace GeneticAlgorithms.ExampleClasses
{
    /// <summary>
    /// A gene with an integer value
    /// </summary>
    public class IntGene : IGene
    {
        /// <summary>
        /// The highest value this IntGene can represent
        /// </summary>
        public const int MaxValue = 100;

        /// <summary>
        /// A random number provider
        /// </summary>
        private static Random Randomizer = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The value
        /// </summary>
        protected int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntGene"/> class with a given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public IntGene(int value)
        {
            this._value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntGene"/> class with a random value.
        /// </summary>
        public IntGene()
        {
            this.Mutate();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _value.ToString();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value
        {
            get
            {
                return this._value;
            }
        }
    
        /// <summary>
        /// See interface documentation
        /// </summary>
        public void Mutate()
        {
            this._value = Randomizer.Next() % IntGene.MaxValue;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="IntGene"/>.
        /// </summary>
        /// <param name="other">The object to convert.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator IntGene(int other)
        {
            return new IntGene(other);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="IntGene"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="other">The object to convert.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator int(IntGene other)
        {
            return other._value;
        }

        /// <summary>
        /// See interface documentation
        /// </summary>
        public object Clone()
        {
            return new IntGene(this._value);
        }

        /// <summary>
        /// See interface documentation
        /// </summary>
        new public bool Equals(object o)
        {
            return (o as IntGene)._value == this._value;
        }
    }

}
