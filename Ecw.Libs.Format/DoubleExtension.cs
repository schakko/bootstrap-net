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

    public static class DoubleExtension
    {
        /// <summary>
        ///   Gibt Stunden die als double übergebenwerden als Zeitstunden 
        ///   zurück. -12,1 => -1:06
        /// </summary>
        /// <param name = "hours"></param>
        /// <returns></returns>
        [Obsolete("use ToHoursAndMinutes")]
        public static string ToStringTime(this double hours)
        {
            int stunden = 0;
            int minuten = 0;
            string vorzeichen = null;

            //Bei negativen Zahlen: das Vorzeichen merken und mit dem Absolutwert der Zahl weiterrechnen.
            if (Math.Sign(hours) == -1)
            {
                hours = Math.Abs(hours);
                vorzeichen = "−";
            }
            else
            {
                vorzeichen = "";
            }

            stunden = (int) Math.Floor(hours);
            minuten = (int) ((hours - Math.Truncate(hours))*60);

            //Aufgrund von Rundungsfehler kann es bei den Minuten zu 60 Minuten kommen. Die Korrektur erfolgt hier.
            if ((minuten == 60))
            {
                stunden = stunden + 1;
                minuten = 0;
            }

            return vorzeichen + stunden + ":" + minuten.ToString("00");
        }


        /// <summary>
        ///   Gibt eine Industriestunden als Stunden und Minuten zurück. Die Minuten sind gerunded.
        /// </summary>
        /// <param name = "hours">Industriestunden</param>
        /// <returns>Eine Zeichenkette Stunden und Minuten durch einen Doppelpunkt getrennt (hh:mm).</returns>
        public static string ToHoursAndMinutes(this double hours)
        {
            string vorzeichen = hours < 0 ? "–" : "";
            double abs = Math.Abs(hours);


            //Bei negativen Zahlen: das Vorzeichen merken und mit dem Absolutwert der Zahl weiterrechnen.
            int stunden = (int) Math.Truncate(abs);
            int minuten = (int) Math.Round((abs - stunden)*60);


            //Aufgrund von Rundungsfehler kann es bei den Minuten zu 60 Minuten kommen. Die Korrektur erfolgt hier.
            if (minuten == 60)
            {
                stunden++;
                minuten = 0;
            }

            return vorzeichen + stunden + ":" + (minuten).ToString("00");
        }
    }
}