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
namespace Ecw.Libs.Export
{
    using System;
    using System.Globalization;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Format;

    public class DateTimeFormats
    {
        public static Formater<DateTime?> Time
        {
            get { return new Formater<DateTime?>("t", 1); }
        }

        public static Formater<DateTime?> TimeSeconds
        {
            get { return new Formater<DateTime?>("T", 2); }
        }

        public static Formater<DateTime?> TimeSecondsMilliseconds
        {
            get { return new Formater<DateTime?>("hh:mm:ss:fff", 3); }
        }

        public static Formater<DateTime?> Date
        {
            get { return new Formater<DateTime?>("d", 4); }
        }

        public static Formater<DateTime?> DateTime
        {
            get { return new Formater<DateTime?>("g", 5); }
        }

        public static Formater<DateTime?> DateTimeSeconds
        {
            get { return new Formater<DateTime?>("G", 6); }
        }

        public static Formater<DateTime?> DateTimeSecondsMilliseconds
        {
            get { return new Formater<DateTime?>("dd:MM:yyyy hh:mm:ss:fff", 7); }
        }
    }

    public class TimeSpanFormats
    {
        public static Formater<TimeSpan?> HoursMinutes
        {
            get { return new Formater<TimeSpan?>("t", 8); }
        }

        public static Formater<TimeSpan?> HoursMinutesSeconds
        {
            get { return new Formater<TimeSpan?>("T", 9); }
        }

        public static Formater<TimeSpan?> HoursMinutesSecondsMilliseconds
        {
            get { return new Formater<TimeSpan?>("HH:mm:ss.fff", 10); }
        }

        //public static Formater<TimeSpan> DaysHours
        //{
        //    get { return new Formater<TimeSpan>("d", 10); }
        //}       
        //public static Formater<TimeSpan> DaysMinutes
        //{
        //    get { return new Formater<TimeSpan>("g", 3); }
        //}
        //public static Formater<TimeSpan> DaysSeconds
        //{
        //    get { return new Formater<TimeSpan>("G", 2); }
        //}

        //public static Formater<TimeSpan> DaysMilliseconds
        //{
        //    get { return new Formater<TimeSpan>(null, 2);  }
        //}
    }


    public class Formater<TColumn>
    {
        public Func<object, object> GetExcelValue;
        public Func<object, string> GetStringValue;
        public Func<object, Cell> GetXlsxCell;

        public Formater()
            : this(null, 0) {}

        public Formater(string formatstring, uint xlsxIndex)
        {
            if (typeof (TColumn) == typeof (TimeSpan))
            {
                if (formatstring == null)
                {
                    formatstring = "T";
                }
                this.GetStringValue = value => ((TimeSpan) value).ToString(formatstring);
                this.GetExcelValue = value => ((TimeSpan) value).ToString(formatstring);
            }
            else if (typeof (TColumn) == typeof (TimeSpan?))
            {
                if (formatstring == null)
                {
                    formatstring = "T";
                }
                this.GetStringValue = value => ((TimeSpan?) value).ToString(formatstring);
                this.GetExcelValue = value => ((TimeSpan?) value).ToString(formatstring);
            }
            else
            {
                this.GetStringValue = value => string.Format(formatstring == null ? "{0}" : "{0:" + formatstring + "}", value);
                if (typeof (TColumn) == typeof (short) || typeof (TColumn) == typeof (short?)
                    || typeof (TColumn) == typeof (ushort) || typeof (TColumn) == typeof (ushort?)
                    || typeof (TColumn) == typeof (int) || typeof (TColumn) == typeof (int?)
                    || typeof (TColumn) == typeof (uint) || typeof (TColumn) == typeof (uint?)
                    || typeof (TColumn) == typeof (long) || typeof (TColumn) == typeof (long?)
                    || typeof (TColumn) == typeof (ulong) || typeof (TColumn) == typeof (ulong?)
                    || typeof (TColumn) == typeof (float) || typeof (TColumn) == typeof (float?)
                    || typeof (TColumn) == typeof (double) || typeof (TColumn) == typeof (double?)
                    || typeof (TColumn) == typeof (DateTime) || typeof (TColumn) == typeof (DateTime?)
                    || typeof (TColumn) == typeof (string)
                    )
                {
                    this.GetExcelValue = value => value;
                }
                else if (typeof (TColumn) == typeof (bool) || typeof (TColumn) == typeof (bool?))
                {
                    this.GetExcelValue = value => ((bool?) value) == null ? (bool?) null : ((bool?) value).Value;
                }
                else
                {
                    this.GetExcelValue = value => String.Format("{0}", value);
                }
            }

            this.GetXlsxCell = GetXlsxCellMethod(xlsxIndex);
        }

        private Formater(Func<object, string> getStringValue, Func<object, Cell> getXlsxCell, Func<object, object> getExcelValue)
        {
            this.GetXlsxCell = getXlsxCell;
            this.GetStringValue = getStringValue;
            this.GetExcelValue = getExcelValue;
        }

        private Func<object, Cell> GetXlsxCellMethod(uint xlsxCellStyleIndex)
        {
            Func<object, Cell> result = null;

            if (typeof (TColumn) == typeof (short) || typeof (TColumn) == typeof (short?)
                || typeof (TColumn) == typeof (ushort) || typeof (TColumn) == typeof (ushort?)
                || typeof (TColumn) == typeof (int) || typeof (TColumn) == typeof (int?)
                || typeof (TColumn) == typeof (uint) || typeof (TColumn) == typeof (uint?)
                || typeof (TColumn) == typeof (long) || typeof (TColumn) == typeof (long?)
                || typeof (TColumn) == typeof (ulong) || typeof (TColumn) == typeof (ulong?)
                || typeof (TColumn) == typeof (float) || typeof (TColumn) == typeof (float?)
                || typeof (TColumn) == typeof (double) || typeof (TColumn) == typeof (double?))
            {
                result = delegate(object value)
                {
                    var cell = new Cell();
                    cell.DataType = CellValues.Number;
                    cell.Append(new CellValue(string.Format(CultureInfo.InvariantCulture, "{0}", value)));
                    cell.StyleIndex = xlsxCellStyleIndex;
                    return cell;
                };
            }
            else if (typeof (TColumn) == typeof (DateTime))
            {
                if (xlsxCellStyleIndex == 0)
                {
                    xlsxCellStyleIndex = 5;
                }

                result = delegate(object value)
                {
                    var cell = new Cell();
                    cell.DataType = CellValues.Date;
                    cell.Append(new CellValue(string.Format(CultureInfo.InvariantCulture, "{0}", ((DateTime) value).ToOADate())));
                    cell.StyleIndex = xlsxCellStyleIndex;
                    return cell;
                };
            }
            else if (typeof (TColumn) == typeof (DateTime?))
            {
                if (xlsxCellStyleIndex == 0)
                {
                    xlsxCellStyleIndex = 5;
                }

                result = delegate(object value)
                {
                    var datetime = (DateTime?) value;
                    var cell = new Cell();
                    cell.DataType = CellValues.Date;
                    cell.Append(new CellValue(datetime.HasValue ? string.Format(CultureInfo.InvariantCulture, "{0}", datetime.Value.ToOADate()) : string.Empty));
                    cell.StyleIndex = xlsxCellStyleIndex;
                    return cell;
                };
            }
            else if (typeof (TColumn) == typeof (TimeSpan))
            {
                if (xlsxCellStyleIndex == 0)
                {
                    xlsxCellStyleIndex = 8;
                }

                result = delegate(object value)
                {
                    var cell = new Cell();
                    cell.DataType = CellValues.Date;
                    cell.Append(new CellValue(string.Format(CultureInfo.InvariantCulture, "{0}", ((TimeSpan) value).TotalDays)));
                    cell.StyleIndex = xlsxCellStyleIndex;
                    return cell;
                };
            }
            else if (typeof (TColumn) == typeof (TimeSpan?))
            {
                if (xlsxCellStyleIndex == 0)
                {
                    xlsxCellStyleIndex = 8;
                }

                result = delegate(object value)
                {
                    var timespan = (TimeSpan?) value;
                    var cell = new Cell();
                    cell.DataType = CellValues.Date;
                    cell.Append(new CellValue(timespan.HasValue ? string.Format(CultureInfo.InvariantCulture, "{0}", timespan.Value.TotalDays) : string.Empty));
                    cell.StyleIndex = xlsxCellStyleIndex;
                    return cell;
                };
            }
            else if (typeof (TColumn) == typeof (bool))
            {
                result = delegate(object value)
                {
                    var cell = new Cell();
                    cell.DataType = CellValues.Boolean;
                    cell.Append(new CellValue((bool) value ? "1" : "0"));
                    cell.StyleIndex = xlsxCellStyleIndex;
                    return cell;
                };
            }
            else if (typeof (TColumn) == typeof (bool?))
            {
                result = delegate(object value)
                {
                    var boolvalue = (bool?) value;
                    var cell = new Cell();
                    cell.DataType = CellValues.Boolean;
                    cell.Append(new CellValue(boolvalue == null ? string.Empty : boolvalue.Value ? "1" : "0"));
                    cell.StyleIndex = xlsxCellStyleIndex;
                    return cell;
                };
            }
            else
            {
                result = delegate(object value)
                {
                    var cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.Append(new CellValue(string.Format("{0}", value)));
                    cell.StyleIndex = xlsxCellStyleIndex;
                    return cell;
                };
            }

            return result;
        }

        public Formater<object> Convert()
        {
            return new Formater<object>(this.GetStringValue, this.GetXlsxCell, this.GetExcelValue);
        }
    }
}