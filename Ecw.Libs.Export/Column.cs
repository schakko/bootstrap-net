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
    using DocumentFormat.OpenXml.Spreadsheet;

    public class Column<TSource, TColumn>
    {
        public Column(string name, Func<TSource, TColumn> value)
            : this(name, value, null) {}

        public Column(string name, Func<TSource, TColumn> value, Formater<TColumn> formater)
        {
            Name = name;
            GetValue = value;
            Formater = formater ?? new Formater<TColumn>();
        }

        public string Name { get; set; }

        private Func<TSource, TColumn> GetValue { get; set; }

        private Formater<TColumn> Formater { get; set; }


        public string GetStringValue(TSource value)
        {
            return Formater.GetStringValue(GetValue(value));
        }

        public object GetExcelValue(TSource value)
        {
            return Formater.GetExcelValue(GetValue(value));
        }

        public Cell GetXmlsCell(TSource value)
        {
            return Formater.GetXlsxCell(GetValue(value));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}