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

    public static class DateTimeExtensions
    {
        /// <summary>
        ///   Gibt Datum und Uhrzeit als String zurück,
        ///   wenn die Tageszeit O ist, wird nur das 
        ///   Datum zurückgegeben.
        /// </summary>
        /// <param name = "value"></param>
        /// <returns></returns>
        public static string ToStringBegin(this DateTime value)
        {
            return value.TimeOfDay.Ticks == 0 ? value.ToString("d") : value.ToString("g");
        }


        /// <summary>
        ///   Gibt Datum und Uhrzeit als String zurück,
        ///   wenn die Tageszeit O ist, wird nur das 
        ///   Datum vom Vortag zurückgegeben.
        /// </summary>
        /// <param name = "value"></param>
        /// <returns></returns>
        public static string ToStringEnd(this DateTime value)
        {
            return value.TimeOfDay.Ticks == 0 ? value.AddDays(-1).ToString("d") : value.ToString("g");
        }


        /// <summary>
        ///   Gibt Datum und Uhrzeit als String zurück,
        ///   wenn die Tageszeit O ist, wird nur das 
        ///   Datum zurückgegeben.
        ///   Ist der Wert null, dann wird ein leerer String zurückgegeben.
        /// </summary>
        /// <param name = "value"></param>
        /// <returns></returns>
        public static string ToStringBegin(this DateTime? value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value.Value.ToStringBegin();
            }
        }

        /// <summary>
        ///   Gibt Datum und Uhrzeit als String zurück,
        ///   wenn die Tageszeit O ist, wird nur das 
        ///   Datum vom Vortag zurückgegeben.
        ///   Ist der Wert null, dann wird ein leerer String zurückgegeben.
        /// </summary>
        /// <param name = "value"></param>
        /// <returns></returns>
        public static string ToStringEnd(this DateTime? value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value.Value.ToStringEnd();
            }
        }
    }
}