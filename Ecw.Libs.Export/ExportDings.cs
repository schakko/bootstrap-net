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
    using System.Collections.Generic;
    using System.Globalization;

    public class ExportConfig
    {
        public string Name { get; set; }

        public Exporter<T> AddTable<T>(IEnumerable<T> itmes)
        {
            return new Exporter<T>();
        }

        public ColumnFoo AddColumn()
        {
            return new ColumnFoo();
        }
    }

    public class ColumnFoo
    {
        public ColumnFoo AddCell()
        {
            return this;
        }
    }

    //internal interface IExport
    //{
    //    void WriteValue(object value);
    //    void NextLine();
    //}

    public class CsvExport : IExport
    {
        public CsvExport(string path, string delimiter, string decimalSeperator)
        {
            Path = path;
            Culture = (CultureInfo) CultureInfo.CurrentCulture.Clone();
            Culture.TextInfo.ListSeparator = delimiter;
            Culture.NumberFormat.CurrencyDecimalSeparator = decimalSeperator;
            Culture.NumberFormat.NumberDecimalSeparator = decimalSeperator;
        }

        public CsvExport(string path, CultureInfo culture)
        {
            Path = path;
            Culture = culture;
        }

        private CultureInfo Culture { get; set; }
        private string Path { get; set; }

        #region IExport Members

        public void WriteValue(object value)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void NextLine()
        {
            throw new NotImplementedException();
        }

        #region IExport Member

        public void NextRow()
        {
            throw new NotImplementedException();
        }

        public void NextCell()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    internal class Test
    {
        public void Testo()
        {
            var x = new ExportConfig();
            x.AddColumn().AddCell().AddCell().AddCell();
            x.AddTable(new string[0]).AddColumn("Value", z => z)
                .AddColumn("Länge", z => z.Length);
            x.AddTable(new DateTime[0]).AddColumn("Hour", z => z.Hour)
                .AddColumn("Minute", z => z.Minute)
                .AddColumn("Second", z => z.Second);
        }
    }
}