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

    ///<summary>
    ///  Erweiterungen für IEqualityComparer
    ///</summary>
    public static class EqualityComparerExtensions
    {
        /// <summary>
        ///   Erstellt ein SequenzComparer.
        /// </summary>
        /// <typeparam name = "T">Typ der Elemente der Squenz</typeparam>
        /// <param name = "equalityComparer">Der Comparer für eine einzelnenes Element der Sequenz.</param>
        /// <returns>Ein Objekt welches eine Sequenz vergleicht.</returns>
        public static SequenceComparer<T> ToSequenceComparer<T>(this IEqualityComparer<T> equalityComparer)
        {
            return new SequenceComparer<T>(equalityComparer);
        }
    }
}