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
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;

    public static class XlsxExporter
    {
        public static XlsxExporter<TSource> New<TSource>(IEnumerable<TSource> source, string name, string path)
        {
            return new XlsxExporter<TSource>(name, path) {Source = source};
        }
    }

    public class XlsxExporter<TSource> : Exporter<TSource>
    {
        private readonly string name;
        private readonly string path;

        public XlsxExporter(string name, string path)
        {
            this.path = path;
            this.name = name;
        }

        public override void Export()
        {
            using (var s = SpreadsheetDocument.Create(this.path, SpreadsheetDocumentType.Workbook))
            {
                // Daten
                var sheetData = new SheetData();
                uint rowIndex = 1;
                sheetData.AppendChild(CreateHeaderRow(rowIndex++, Columns));
                foreach (var item in Source)
                {
                    sheetData.AppendChild(CreateContentRow(rowIndex++, item, Columns));
                }

                var workbookPart = s.AddWorkbookPart();
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                string relId = workbookPart.GetIdOfPart(worksheetPart);


                var workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                workbookStylesPart.Stylesheet = CreateStylesheet();
                workbookStylesPart.Stylesheet.Save();


                var worksheet = new Worksheet();
                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;
                worksheetPart.Worksheet.Save();


                var sheet = new Sheet
                {
                    Name = this.name,
                    SheetId = 1,
                    Id = relId
                };

                var sheets = new Sheets();
                sheets.Append(sheet);

                var workbook = new Workbook();
                var fileVersion = new FileVersion {ApplicationName = "Microsoft Office Excel"};
                workbook.Append(fileVersion);
                workbook.Append(sheets);
                s.WorkbookPart.Workbook = workbook;
                s.WorkbookPart.Workbook.Save();
            }
        }

        private static Row CreateContentRow(uint rowIndex, TSource item, IList<Column<TSource, object>> columns)
        {
            var row = new Row {RowIndex = rowIndex};
            int columnIndex = 1;
            foreach (var column in columns)
            {
                //row.Append(CreateCell(i++, index, column.GetValue(item)));
                var cell = column.GetXmlsCell(item);
                cell.CellReference = IndexToHeader(columnIndex++) + rowIndex;
                row.Append(cell);
            }
            return row;
        }

        private static Row CreateHeaderRow(uint index, IList<Column<TSource, object>> columns)
        {
            var row = new Row {RowIndex = index};
            int i = 1;
            foreach (var column in columns)
            {
                row.Append(CreateTextCell(i++, index, column.Name));
            }
            return row;
        }

        private static string IndexToHeader(int index)
        {
            index--;
            if (index < 26)
            {
                return ((char) (index + 'A')).ToString();
            }
            else
            {
                return IndexToHeader(index/26) + IndexToHeader(index%26);
            }
        }


        private static Cell CreateTextCell(int columnIndex, UInt32 rowIndex, string text)
        {
            var c = new Cell {DataType = CellValues.InlineString, CellReference = IndexToHeader(columnIndex) + rowIndex};
            var istring = new InlineString();
            var t = new Text {Text = text};
            istring.Append(t);
            c.Append(istring);
            return c;
        }


        private static Stylesheet CreateStylesheet()
        {
            var ss = new Stylesheet();

            var fts = new Fonts();
            var ft = new Font();
            var ftn = new FontName();
            ftn.Val = "Calibri";
            var ftsz = new FontSize();
            ftsz.Val = 11;
            ft.FontName = ftn;
            ft.FontSize = ftsz;
            fts.Append(ft);
            fts.Count = (uint) fts.ChildElements.Count;

            var fills = new Fills();
            Fill fill;
            PatternFill patternFill;
            fill = new Fill();
            patternFill = new PatternFill();
            patternFill.PatternType = PatternValues.None;
            fill.PatternFill = patternFill;
            fills.Append(fill);
            fill = new Fill();
            patternFill = new PatternFill();
            patternFill.PatternType = PatternValues.Gray125;
            fill.PatternFill = patternFill;
            fills.Append(fill);
            fills.Count = (uint) fills.ChildElements.Count;

            var borders = new Borders();
            var border = new Border();
            border.LeftBorder = new LeftBorder();
            border.RightBorder = new RightBorder();
            border.TopBorder = new TopBorder();
            border.BottomBorder = new BottomBorder();
            border.DiagonalBorder = new DiagonalBorder();
            borders.Append(border);
            borders.Count = (uint) borders.ChildElements.Count;

            var csfs = new CellStyleFormats();
            var cf = new CellFormat();
            cf.NumberFormatId = 0;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            csfs.Append(cf);
            csfs.Count = (uint) csfs.ChildElements.Count;


            var cellFormats = new CellFormats();

            // 0
            cf = new CellFormat();
            cf.NumberFormatId = 0;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cellFormats.Append(cf);

            // 1
            // hh:mm
            cf = new CellFormat();
            cf.NumberFormatId = 20;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);


            // 2
            // hh:mm:ss
            cf = new CellFormat();
            cf.NumberFormatId = 21;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);

            // 3
            // hh:mm:ss.000
            cf = new CellFormat();
            cf.NumberFormatId = 21;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);


            // 4
            // dd/mm/yyyy
            cf = new CellFormat();
            cf.NumberFormatId = 14;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);

            // 5
            // dd/mm/yyyy hh:mm
            cf = new CellFormat();
            cf.NumberFormatId = 22;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);

            // 6 dd/mm/yyyy hh:mm:ss
            uint iExcelIndex = 164;
            var numberingFormats = new NumberingFormats();
            NumberingFormat numberingFormat;
            numberingFormat = new NumberingFormat();
            numberingFormat.NumberFormatId = iExcelIndex++;
            numberingFormat.FormatCode = "dd/mm/yyyy hh:mm:ss";
            numberingFormats.Append(numberingFormat);
            cf = new CellFormat();
            cf.NumberFormatId = numberingFormat.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);

            // 7 dd/mm/yyyy hh:mm:ss.000
            numberingFormat = new NumberingFormat();
            numberingFormat.NumberFormatId = iExcelIndex++;
            numberingFormat.FormatCode = "dd/mm/yyyy hh:mm:ss.000";
            numberingFormats.Append(numberingFormat);
            cf = new CellFormat();
            cf.NumberFormatId = numberingFormat.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);

            // 8
            // [hh]:mm
            numberingFormat = new NumberingFormat();
            numberingFormat.NumberFormatId = iExcelIndex++;
            numberingFormat.FormatCode = "[hh]:mm";
            numberingFormats.Append(numberingFormat);
            cf = new CellFormat();
            cf.NumberFormatId = numberingFormat.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);


            // 9
            // [hh]:mm:ss
            numberingFormat = new NumberingFormat();
            numberingFormat.NumberFormatId = iExcelIndex++;
            numberingFormat.FormatCode = "[hh]:mm:ss";
            numberingFormats.Append(numberingFormat);
            cf = new CellFormat();
            cf.NumberFormatId = numberingFormat.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);

            // 10
            // [hh]:mm:ss.000
            numberingFormat = new NumberingFormat();
            numberingFormat.NumberFormatId = iExcelIndex++;
            numberingFormat.FormatCode = "[hh]:mm:ss.000";
            numberingFormats.Append(numberingFormat);
            cf = new CellFormat();
            cf.NumberFormatId = numberingFormat.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = true;
            cellFormats.Append(cf);


            numberingFormats.Count = (uint) numberingFormats.ChildElements.Count;
            cellFormats.Count = (uint) cellFormats.ChildElements.Count;

            ss.Append(numberingFormats);
            ss.Append(fts);
            ss.Append(fills);
            ss.Append(borders);
            ss.Append(csfs);
            ss.Append(cellFormats);

            var css = new CellStyles();
            var cs = new CellStyle();
            cs.Name = "Normal";
            cs.FormatId = 0;
            cs.BuiltinId = 0;
            css.Append(cs);
            css.Count = (uint) css.ChildElements.Count;
            ss.Append(css);

            var dfs = new DifferentialFormats();
            dfs.Count = 0;
            ss.Append(dfs);

            var tss = new TableStyles();
            tss.Count = 0;
            tss.DefaultTableStyle = "TableStyleMedium9";
            tss.DefaultPivotStyle = "PivotStyleLight16";
            ss.Append(tss);

            return ss;
        }
    }
}