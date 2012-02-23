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
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///   Klasse zum Vergleichen von zwei Objekten im Rahmen eines Unittest.
    ///   Wenn Abweichungen gefünden werden, wird eine Exception geworfen.
    /// </summary>
    /// <typeparam name = "T">Typ der Objektes welches verglichen werden soll</typeparam>
    public class ObjectComparer<T> : IEqualityComparer<T>
    {
        private readonly List<Func<T, T, bool>> navigationPropertyComparers = new List<Func<T, T, bool>>();
        private readonly List<KeyValuePair<string, Func<T, object>>> propertySelectors = new List<KeyValuePair<string, Func<T, object>>>();

        #region IEqualityComparer<T> Members

        /// <summary>
        ///   Prüft ob die Eigenschaften von zwei Objekten gleich sind.
        ///   Wenn einen Eigenschaft nicht gleich ist, wird eine Exception geworfen.
        /// </summary>
        /// <param name = "expected">Der Erwartungswert</param>
        /// <param name = "actual">Der Prüfling</param>
        /// <exception cref = "PropertyEqualException">Falls eine Eigenschaft nicht gleich ist, wird diese Exception geworfen.</exception>
        /// <returns>true wenn die Objekte gleich sind, wenn nicht wird eine Exception geworfen</returns>
        public bool Equals(T expected, T actual)
        {
            // Wenn beide Objekte null sind oder die gleiche Referenz haben, dann sind sie gleich
            if (ReferenceEquals(expected, actual))
            {
                return true;
            }
            // Wenn nur eines der beiden Objekte null ist, dann sind sie ungleich
            if (ReferenceEquals(expected, null) || ReferenceEquals(actual, null))
            {
                return false;
            }

            foreach (var selector in this.propertySelectors)
            {
                var expectedProperty = selector.Value(expected);
                var actualProperty = selector.Value(actual);

                // Wenn beide Objekte null sind oder die gleiche Referenz haben, dann sind sie gleich
                if (ReferenceEquals(expectedProperty, actualProperty))
                {
                    continue;
                }

                if (ReferenceEquals(expectedProperty, null) || !expectedProperty.Equals(actualProperty))
                {
                    throw new PropertyEqualException(selector.Key, expectedProperty, actualProperty);
                }
            }

            foreach (var comparer in this.navigationPropertyComparers)
            {
                comparer(expected, actual);
            }
            return true;
        }

        /// <summary>
        ///   Gibt einen Hashcode für das angegebene Objekt zurück.
        /// </summary>
        /// <returns>
        ///   Ein Hashcode für das angegebene Objekt.
        /// </returns>
        /// <param name = "obj">Das <see cref = "T:System.Object" />, für das ein Hashcode zurückgegeben werden soll.</param>
        /// <exception cref = "T:System.ArgumentNullException">Der Typ von <paramref name = "obj" /> ist ein Verweistyp, und <paramref name = "obj" /> ist null.</exception>
        public int GetHashCode(T obj)
        {
            return obj == null ? 0 : obj.GetHashCode();
        }

        #endregion

        /// <summary>
        ///   Fügt eine neue Eigenschaft hinzu die verglichen wird.
        /// </summary>
        /// <param name = "name">Name der Eigenschaft der im Falle einer Abweichung in der Exception genannt wird</param>
        /// <param name = "propertySelector">Eigenschaft die Verglichen wird</param>
        public ObjectComparer<T> Add(string name, Func<T, object> propertySelector)
        {
            this.propertySelectors.Add(new KeyValuePair<string, Func<T, object>>(name, propertySelector));
            return this;
        }

        /// <summary>
        ///   Fügt eine neue Eigenschaft hinzu die mit hilfer des übergeben IEqualityCompares verglichen wird.
        /// </summary>
        /// <typeparam name = "TProperty">Typ der Eigenschaft die verglichen werden soll</typeparam>
        /// <param name = "propertySelector">Eigenschaft die verglichen wird.</param>
        /// <param name = "propertyComparer">IEqualitiyComperer der die Eigenschaften vergleicht.</param>
        public ObjectComparer<T> Add<TProperty>(Func<T, TProperty> propertySelector, IEqualityComparer<TProperty> propertyComparer)
        {
            this.navigationPropertyComparers.Add((a, b) => propertyComparer.Equals(propertySelector(a), propertySelector(b)));
            return this;
        }
    }
}