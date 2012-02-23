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
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public static class ClipboardExporter
    {
        public static ClipboardExporter<TSource> New<TSource>(IEnumerable<TSource> source)
        {
            return new ClipboardExporter<TSource>(source);
        }
    }


    public class ClipboardExporter<TSource> : Exporter<TSource>
    {
        public ClipboardExporter() {}

        public ClipboardExporter(IEnumerable<TSource> source)
        {
            Source = source;
        }


        public override void Export()
        {
            if (Columns == null || Columns.Count == 0)
            {
                throw new Exception("Keine Spalten zum Exportieren angegeben.");
            }

            bool first = true;

            var builder = new StringBuilder("");

            foreach (var column in Columns)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append('\t');
                }
                builder.Append(column.Name);
            }


            foreach (var item in Source)
            {
                builder.Append('\n');

                first = true;
                foreach (var column in Columns)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        builder.Append('\t');
                    }
                    builder.Append(column.GetStringValue(item));
                }
            }
            try
            {
                Clipboard.SetData(DataFormats.Text, builder.ToString());
            }
            catch (COMException)
            {
                Thread.Sleep(1000);
                try
                {
                    Clipboard.SetData(DataFormats.Text, builder.ToString());
                }
                catch (COMException)
                {
                    MessageBox.Show("Kein Zugriff auf die Zwischenablage");
                }
            }
        }
    }
}