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
    using System.Globalization;

    public static class TimeSpanExtensions
    {
        /// <summary>
        ///   Konvertiernt den Wert dieser Instanz unter Verwendung der angegebenen 
        ///   Formatierungsinformationen in die entsprechende Zeichenfolge.
        ///   <example>
        ///     timespan = new TimeSpan(1,2,3);
        ///     t.ToString("t") == "02:03"  
        ///     t.ToString("g") == "1:02:03"
        ///   </example>
        /// </summary>
        /// <param name = "value"></param>
        /// <param name = "format">
        ///   Eine Formatierugszeichenfolge
        ///   Mögliche Werte: 
        ///   "t" -> "HH:mm", 
        ///   "T" -> "HH:mm:ss"
        ///   "d" -> "d:HH"
        ///   "g" -> "d:HH:mm", 
        ///   "G" -> "d:HH:mm:ss"
        /// </param>
        /// <returns></returns>
        public static string ToString(this TimeSpan value, string format)
        {
            string sign = value.Ticks < 0 ? "-" : "";
            value = new TimeSpan(Math.Abs(value.Ticks));

            int totalHours = value.Hours + value.Days*24;

            if (format == "t" || format == "HH:mm")
            {
                return sign + totalHours.ToString("00") + ":" + value.Minutes.ToString("00");
            }
            else if (format == "T" || format == "HH:mm:ss")
            {
                return sign + totalHours.ToString("00") + ":" + value.Minutes.ToString("00") + ":" + value.Seconds.ToString("00");
            }
            else if (format == "d" || format == "d:HH")
            {
                return sign + value.Days + ":" + value.Hours.ToString("00");
            }
            else if (format == "g" || format == "d:HH:mm")
            {
                return sign + value.Days + ":" + value.Hours.ToString("00") + ":" + value.Minutes.ToString("00");
            }
            else if (format == "G" || format == "d:HH:mm:ss")
            {
                return sign + value.Days + ":" + value.Hours.ToString("00") + ":" + value.Minutes.ToString("00") + ":" + value.Seconds.ToString("00");
            }
            else if (format == "HH:mm:ss.fff")
            {
                return sign + totalHours.ToString("00") + ":" + value.Minutes.ToString("00") + ":" + value.Seconds.ToString("00")
                       + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + value.Milliseconds.ToString("000");
            }
            else // (format == "d:HH:mm:ss.fff")
            {
                return value.ToString();
            }
        }
    }
}