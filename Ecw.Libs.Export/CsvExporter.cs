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
    using System.IO;
    using System.Text;

    public static class CsvExporter
    {
        public static CsvExporter<TSource> New<TSource>(IEnumerable<TSource> source, string path)
        {
            return new CsvExporter<TSource>(path, CultureInfo.CurrentCulture) {Source = source};
        }

        public static CsvExporter<TSource> New<TSource>(IEnumerable<TSource> source, string path, string delimiter, string decimalSeperator)
        {
            return new CsvExporter<TSource>(path, delimiter, decimalSeperator) {Source = source};
        }

        public static CsvExporter<TSource> New<TSource>(IEnumerable<TSource> source, string path, CultureInfo culture)
        {
            return new CsvExporter<TSource>(path, culture) {Source = source};
        }
    }

    public class CsvExporter<TSource> : Exporter<TSource>
    {
        public CsvExporter(string path, string delimiter, string decimalSeperator)
        {
            Path = path;
            Culture = (CultureInfo) CultureInfo.CurrentCulture.Clone();
            Culture.TextInfo.ListSeparator = delimiter;
            Culture.NumberFormat.CurrencyDecimalSeparator = decimalSeperator;
            Culture.NumberFormat.NumberDecimalSeparator = decimalSeperator;
        }

        public CsvExporter(string path, CultureInfo culture)
        {
            Path = path;
            Culture = culture;
        }

        private CultureInfo Culture { get; set; }
        private string Path { get; set; }

        public override void Export()
        {
            if (Columns == null || Columns.Count == 0)
            {
                throw new Exception("Keine Spalten zum Exportieren angegeben.");
            }

            using (var writer = File.CreateText(Path))
            {
                var builder = new StringBuilder("");
                string delimiter = Culture.TextInfo.ListSeparator;
                int length = delimiter.Length;

                foreach (var column in Columns)
                {
                    builder.Append(delimiter);
                    builder.Append(column.Name);
                }

                // Remove the first delemiter
                writer.WriteLine(builder.ToString().Remove(0, length));

                foreach (var item in Source)
                {
                    builder = new StringBuilder("");
                    foreach (var column in Columns)
                    {
                        builder.Append(delimiter);
                        //builder.AppendFormat(Culture, column.Formatstring, column.Value(item));
                        builder.Append(column.GetStringValue(item));
                    }
                    writer.WriteLine(builder.ToString().Remove(0, length));
                }
            }
        }
    }
}