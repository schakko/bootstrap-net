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
    using System.Collections.Generic;
    using Microsoft.Office.Interop.Excel;

    public static class ExcelExporter
    {
        public static ExcelExporter<TSource> New<TSource>(IEnumerable<TSource> source)
        {
            return new ExcelExporter<TSource> {Source = source};
        }
    }


    /// <summary>
    ///   Klasse um Daten nach Excel zu exportieren
    /// </summary>
    public class ExcelExporter<TSource> : Exporter<TSource>
    {
        /// <summary>
        ///   Schreibt den gesamten Inhalt der übergebenen DataTable 
        ///   mit Spaltenüberschriften in eine Exceltabelle.
        ///   Anschließend wird die Excel mit der Tabelle angezeigt.
        /// </summary>
        /// <param name = "dt"></param>
        public override void Export()
        {
            ApplicationClass excel = null;

            try
            {
                excel = new ApplicationClass();
                var workbook = excel.Application.Workbooks.Add(true);


                int columnIndex = 0;
                foreach (var column in this.columns)
                {
                    int rowIndex = 1;
                    excel.Cells[rowIndex, ++columnIndex] = column.Name;

                    foreach (var item in Source)
                    {
                        excel.Cells[++rowIndex, columnIndex] = column.GetExcelValue(item);
                    }
                }

                excel.Visible = true;
            }
            catch
            {
                if (excel != null)
                {
                    try
                    {
                        excel.Quit();
                    }
                    catch {}
                }
                throw;
            }
        }
    }
}