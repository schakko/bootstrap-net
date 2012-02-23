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
    using System.Linq;

    public class Exporter<TSource>
    {
        internal IList<Column<TSource, object>> columns = new List<Column<TSource, object>>();

        public IList<Column<TSource, object>> Columns
        {
            get { return this.columns; }
        }

        public IEnumerable<TSource> Source { get; set; }


        public Exporter<TSource> AddColumns(IList<Column<TSource, object>> columns)
        {
            this.columns = Columns.Concat(columns).ToList();
            return this;
        }

        public Exporter<TSource> AddColumn<TColumn>(string name, Func<TSource, TColumn> value)
        {
            AddColumn(name, value, null);
            return this;
        }

        public Exporter<TSource> AddColumn<TColumn>(string name, Func<TSource, TColumn> value, Formater<TColumn> formater)
        {
            // Mittels x => value(x) wird Func<TSource,TColumn> zu Func<TSource,object> konvertiert
            Columns.Add(new Column<TSource, object>(name, x => value(x), (formater ?? new Formater<TColumn>()).Convert()));
            return this;
        }

        //Func<TSource, IEnumerable<object>> Value {get;set;}

        //public Exporter<TMultiColumn> SetMultiColumn<TMultiColumn>(string name, Func<TSource, IEnumerable<TMultiColumn>> value)
        //{
        //    Value = x => value(x).Cast<object>();
        //    return new Exporter<TMultiColumn>();
        //}


        public virtual void Export() {}
    }

    internal interface IExport
    {
        void NextRow();
        void NextCell();
        void WriteValue(object value);
    }
}