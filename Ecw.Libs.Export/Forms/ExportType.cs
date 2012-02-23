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
namespace Ecw.Libs.Export.Forms
{
    public class ExportType
    {
        private int id;

        private ExportType() {}

        public string Name { get; set; }
        public string DeflaultExtension { get; set; }
        public string FilterExtensions { get; set; }

        public bool HasDelimiter { get; private set; }
        public bool HasDecimalSeperator { get; private set; }
        public bool HasPath { get; private set; }


        public static ExportType ClipboardExport
        {
            get
            {
                return new ExportType
                {
                    Name = "In die Zwischenablage kopieren",
                    id = 0,
                };
            }
        }

        public static ExportType ExcelExport
        {
            get
            {
                return new ExportType
                {
                    Name = "Direkt in Excel schreiben",
                    id = 1,
                };
            }
        }

        public static ExportType CsvExport
        {
            get
            {
                return new ExportType
                {
                    Name = "Als Textdatei speichern",
                    DeflaultExtension = "csv",
                    FilterExtensions = @"csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*",
                    HasPath = true,
                    HasDecimalSeperator = true,
                    HasDelimiter = true,
                    id = 2,
                };
            }
        }

        public static ExportType XlsxExport
        {
            get
            {
                return new ExportType
                {
                    Name = "Als Exceldokument speichern",
                    DeflaultExtension = "xlsx",
                    FilterExtensions = @"Excel-Dokument (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    HasPath = true,
                    HasDecimalSeperator = false,
                    id = 3,
                };
            }
        }


        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is ExportType)
            {
                return ((ExportType) obj).id == this.id;
            }
            else
            {
                return false;
            }
        }
    }
}