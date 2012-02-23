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
namespace Ecw.Libs.Utils
{
    using System;
    using System.Globalization;

    internal static class DateHelper
    {
        internal static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            var jan1 = new DateTime(year, 1, 1);

            int daysOffset = (int) firstDayOfWeek - (int) jan1.DayOfWeek;
            if (daysOffset == 1)
            {
                daysOffset = -6;
            }

            var firstMonday = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);

            if (firstWeek == 1)
            {
                weekOfYear -= 1;
            }

            return firstMonday.AddDays(weekOfYear*7);
        }
    }
}