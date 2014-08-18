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
    /// A gene with an boolean value
    /// </summary>
    public class BoolGene : IGene
    {
        /// <summary>
        /// A random number provider
        /// </summary>
        private static Random randomizer = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The value
        /// </summary>
        protected bool _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolGene"/> class with the given value
        /// </summary>
        /// <param name="value">The value.</param>
        public BoolGene(bool value)
        {
            this._value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolGene"/> class with a random value
        /// </summary>
        public BoolGene()
        {
            this._value = Convert.ToBoolean(randomizer.Next(0, 2));
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (this._value)
                return "1";
            else
                return "0";
            //return value.ToString();
        }

        /// <summary>
        /// The value this BoolGene represents
        /// </summary>
        public bool Value
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
            this._value = !this._value;
            //this.value = Convert.ToBoolean(randomizer.Next(0, 2));
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Boolean"/> to <see cref="BoolGene"/>.
        /// </summary>
        /// <param name="other">The object to convert.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator BoolGene(bool other)
        {
            return new BoolGene(other);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="BoolGene"/> to <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="other">The object to convert.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator bool(BoolGene other)
        {
            return other._value;
        }

        /// <summary>
        /// See interface documentation
        /// </summary>
        public object Clone()
        {
            return new BoolGene(this._value);
        }

        /// <summary>
        /// See interface documentation
        /// </summary>
        new public bool Equals(object o)
        {
            return (o as BoolGene)._value == this._value;
        }
    }

}
