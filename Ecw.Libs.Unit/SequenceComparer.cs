// <copyright file="" company="EDV Consulting Wohlers GmbH">
// 	Copyright (C) 2012 EDV Consulting Wohlers GmbH
// 	
// 	This library is free software; you can redistribute it and/or
// 	modify it under the terms of the GNU Lesser General Public
// 	License as published by the Free Software Foundation; either
// 	version 3 of the License, or (at your option) any later version.
// 
// 	This library is distributed in the hope that it will be useful,
// 	but WITHOUT ANY WARRANTY; without even the implied warranty of
// 	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// 	Lesser General Public License for more details.
// 
// 	You should have received a copy of the GNU Lesser General Public
// 	License along with this library. If not, see http://www.gnu.org/licenses/. 
// </copyright>
// <author>Daniel Vogelsang</author>
namespace Ecw.Libs.Unit
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///   Vergleicht eine Sequenz. Die Reichenfolge ist relevant.
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    public class SequenceComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        private readonly IEqualityComparer<T> elementComparer;

        /// <summary>
        ///   Erzeugt eine Instanz der Klasse.
        /// </summary>
        /// <param name = "elementComparer">Comparer für ein Element der Sequenz</param>
        public SequenceComparer(IEqualityComparer<T> elementComparer)
        {
            this.elementComparer = elementComparer;
        }

        #region IEqualityComparer<IEnumerable<T>> Members

        /// <summary>
        ///   Bestimmt, ob die angegebenen Objekte gleich sind.
        /// </summary>
        /// <returns>
        ///   true, wenn die angegebenen Objekte gleich sind, andernfalls false.
        /// </returns>
        /// <param name = "x">Das erste zu vergleichende Objekt vom Typ <paramref name = "T" />.</param>
        /// <param name = "y">Das zweite zu vergleichende Objekt vom Typ <paramref name = "T" />.</param>
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            return x.SequenceEqual(y, this.elementComparer);
        }

        /// <summary>
        ///   Gibt einen Hashcode für das angegebene Objekt zurück.
        /// </summary>
        /// <returns>
        ///   Ein Hashcode für das angegebene Objekt.
        /// </returns>
        /// <param name = "obj">Das <see cref = "T:System.Object" />, für das ein Hashcode zurückgegeben werden soll.</param>
        /// <exception cref = "T:System.ArgumentNullException">Der Typ von <paramref name = "obj" /> ist ein Verweistyp, und <paramref name = "obj" /> ist null.</exception>
        public int GetHashCode(IEnumerable<T> obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}