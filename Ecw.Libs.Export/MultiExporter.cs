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
    using Forms;

    public class MultiExporter<TSource>
    {
        private readonly IList<SelectableColumn<TSource, object>> columns = new List<SelectableColumn<TSource, object>>();


        public string Name { get; set; }

        public string Source { private get; set; }


        public void AddColumn<TColumn>(string name, Func<TSource, TColumn> value)
        {
            AddColumn(name, value, true, null);
        }

        public void AddColumn<TColumn>(string name, Func<TSource, TColumn> value, bool isChecked)
        {
            AddColumn(name, value, isChecked, null);
        }

        public void AddColumn<TColumn>(string name, Func<TSource, TColumn> value, Formater<TColumn> format)
        {
            AddColumn(name, value, true, format);
        }

        public void AddColumn<TColumn>(string name, Func<TSource, TColumn> value, bool isChecked, Formater<TColumn> formater)
        {
            // Mittels x => value(x) wird Func<TSource,TColumn> zu Func<TSource> konvertiert
            this.columns.Add(new SelectableColumn<TSource, object>(name, x => value(x), isChecked, (formater ?? new Formater<TColumn>()).Convert()));
        }

        //public void Export(Exporter< >
    }
}