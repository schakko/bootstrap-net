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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class RecursionException : ApplicationException
    {
        public RecursionException(string message) : base(message) {}
    }

    public class ReflectionComparer : IEqualityComparer<object>, IEqualityComparer
    {
        private readonly int depth;

        public ReflectionComparer(int depth)
        {
            this.depth = depth;
        }

        public ReflectionComparer()
            : this(0) {}

        #region IEqualityComparer<object> Members

        /// <summary>
        ///   Überprüft zwei Objekte und deren Eigenschaften mittels Reflections rekursiv auf Gleichheit.
        ///   Bei Wertetypen (struct) wird immer die Equals-Methode aufgerufen.
        /// </summary>
        /// <param name = "expected">Objekt mit den Werten die Erwartet werden.</param>
        /// <param name = "actual">Objekt welches geprüft werden soll.</param>
        public new bool Equals(object expected, object actual)
        {
            AssertEqualityDeep(expected, actual, 10, 0, null);
            return true;
        }

        public int GetHashCode(object obj)
        {
            return obj == null ? 0 : obj.GetHashCode();
        }

        #endregion

        /// <summary>
        ///   Überprüft zwei Objekte und deren Eigenschaften mittels Reflections rekursiv auf Gleichheit.
        ///   Bei Wertetypen (struct) wird immer die Equals-Methode aufgerufen.
        /// </summary>
        /// <param name = "expected">Objekt mit den Werten die Erwartet werden.</param>
        /// <param name = "actual">Objekt welches geprüft werden soll.</param>
        /// <param name = "limit">Grenze der Verschachtelungstiefe</param>
        /// <param name = "count">Aktuelle Verschachtelungstiefe</param>
        /// <param name = "name">Pfad zur aktuellen Eigenschaft</param>
        private static void AssertEqualityDeep(object expected, object actual, int limit, int count, string name)
        {
            if (expected == null && actual == null)
            {
                return;
            }
            CheckType(expected, actual, null);

            if (expected.GetType().IsValueType || expected.GetType() == typeof (string))
            {
                CheckValue(expected, actual, name);
            }

            var properties = expected.GetType().GetProperties();

            foreach (var property in properties.Where(x => !x.GetIndexParameters().Any()))
            {
                var expectedObject = property.GetValue(expected, null);
                //Console.WriteLine(expectedObject);
                var actualObject = property.GetValue(actual, null);

                if (ReferenceEquals(expectedObject, expected) && ReferenceEquals(actualObject, actual))
                {
                    return;
                }

                if (count > limit)
                {
                    throw new RecursionException(string.Format("Die maximale Verschachtelungstiefe von {0} wurde erreicht.", limit));
                }

                AssertEqualityDeep(expectedObject, actualObject, limit, count + 1, name == null ? property.Name : name + "." + property.Name);
            }
        }

        /// <summary>
        ///   Überprüft zwei Objekte und deren Eigenschaften mittels Reflections auf Gleichheit.
        ///   Bei Wertetypen (struct) wird immer die Equals-Methode aufgerufen.
        /// </summary>
        /// <param name = "expected">Objekt mit den Werten die Erwartet werden.</param>
        /// <param name = "actual">Objekt welches geprüft werden soll.</param>
        private static void AssertEquality(object expected, object actual)
        {
            if (expected == null && actual == null)
            {
                return;
            }
            Check(expected, actual, null);

            var properties = expected.GetType().GetProperties();

            foreach (var property in properties.Where(x => !x.GetIndexParameters().Any()))
            {
                //if (property.GetIndexParameters().Any()) continue; 

                var expectedObject = property.GetValue(expected, null);
                //Console.WriteLine(expectedObject);
                var actualObject = property.GetValue(actual, null);

                if (expectedObject == null && actualObject == null)
                {
                    continue;
                }
                Check(expectedObject, actualObject, property.Name);
            }
        }


        private static void Check(object expected, object actual, string name)
        {
            CheckType(expected, actual, name);
            CheckValue(expected, actual, name);
        }

        private static void CheckValue(object expected, object actual, string name)
        {
            if (expected is IEnumerable)
            {
                return;
            }
            if (!expected.Equals(actual))
            {
                throw new PropertyEqualException(name ?? "root", expected, actual);
            }
        }

        private static void CheckType(object expected, object actual, string name)
        {
            if (expected == null && actual != null || expected != null && actual == null)
            {
                throw new PropertyEqualException(name ?? "root", expected, actual);
            }
            if (expected.GetType() != actual.GetType())
            {
                throw new PropertyEqualException("Type of " + (name ?? "root"), expected.GetType(), actual.GetType());
            }
        }
    }
}