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
namespace Ecw.Libs.Format
{
    using System;

    public static class NullableExtensions
    {
        public static string ToString(this TimeSpan? timeSpan, string format)
        {
            return timeSpan == null ? null : timeSpan.Value.ToString(format);
        }

        /// <summary>
        ///   Wenn das Objekt einen Wert hat, wird für diesen ToString() aufgerufen,
        ///   sonst wird null zurück gegeben.
        /// </summary>
        /// <typeparam name = "T">Ein beliebiger Wertetyp</typeparam>
        /// <param name = "nullabe">Ein Nullable-Objekt</param>
        /// <returns>Das Ergebnis der ToString-Methode der Value-Eigenschaft oder null</returns>
        public static string ToString<T>(this T? nullabe) where T : struct
        {
            return nullabe == null ? null : nullabe.Value.ToString();
        }

        /// <summary>
        ///   Wenn das Objekt einen Wert hat, wird für diesen ToString(format, formatprovider) aufgerufen,
        ///   sonst wird null zurück gegeben.
        /// </summary>
        /// <typeparam name = "T">Ein Wertetyp der die Schnittstelle IFormatable implementiert</typeparam>
        /// <param name = "nullable">Ein Nullable-Objekt</param>
        /// <param name = "format">
        ///   Der Formatstring nachdem die Rückgabe formatiert werden soll.
        /// </param>
        /// <param name = "provider">
        ///   Der Formatprovider oder null, falls der Standardprovider für diesen Typ genutzt werden soll.
        /// </param>
        /// <returns>Das Ergebnis der ToString-Methode der Value-Eigenschaft oder null</returns>
        public static string ToString<T>(this T? nullable, string format) where T : struct, IFormattable
        {
            return nullable == null ? null : nullable.Value.ToString(format, null);
        }


        /// <summary>
        ///   Wenn das Objekt einen Wert hat, wird für diesen ToString(format, formatprovider) aufgerufen,
        ///   sonst wird null zurück gegeben.
        /// </summary>
        /// <typeparam name = "T">Ein Wertetyp der die Schnittstelle IFormatable implementiert</typeparam>
        /// <param name = "nullable">Ein Nullable-Objekt</param>
        /// <param name = "format">
        ///   Der Formatstring nachdem die Rückgabe formatiert werden soll.
        /// </param>
        /// <param name = "provider">
        ///   Der Formatprovider oder null, falls der Standardprovider für diesen Typ genutzt werden soll.
        /// </param>
        /// <returns>Das Ergebnis der ToString-Methode der Value-Eigenschaft oder null</returns>
        public static string ToString<T>(this T? nullable, string format, IFormatProvider provider) where T : struct, IFormattable
        {
            return nullable == null ? null : nullable.Value.ToString(format, provider);
        }
    }
}