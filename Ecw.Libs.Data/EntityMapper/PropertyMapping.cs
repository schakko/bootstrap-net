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
namespace Ecw.Libs.Data.EntityMapper
{
    using System;
    using System.Data;

    /// <summary>
    ///   Mappt die Eigenschaft einer Entität auf eine Spalten in einem Recordset und umgekehrt.
    /// </summary>
    /// <typeparam name = "TEntity">Typ  der Entität</typeparam>
    public class PropertyMapping<TEntity>
    {
        //private readonly Action<TEntity, SqlParameterCollection> addParameter;

        private readonly string columnName;
        private readonly Func<TEntity, object> getProperty;
        private readonly Action<TEntity, int, IDataReader> setProperty;
        //private int ordinal = -1;


        internal PropertyMapping(string columnName, Func<TEntity, object> getter, Action<TEntity, int, IDataReader> setter, bool allowInsert)
        {
            this.columnName = columnName;
            this.setProperty = setter;
            this.getProperty = getter;
            AllowInsert = allowInsert;
        }

        /// <summary>
        ///   Gibt an, ob Werte in dieser Spalte eingefügt werden dürfen. 
        ///   (Falls nicht explizit erlaubt, dürfen in MSSQL-Identity-Spalten keine Werte eingetragen werden.)
        /// </summary>
        public bool AllowInsert { get; private set; }


        /// <summary>
        ///   Gibt den Namen der abgebildeten Spalte an.
        /// </summary>
        public string ColumnName
        {
            get { return this.columnName; }
        }


        /// <summary>
        ///   Gibt die Action zum Zuweisen einer Eigenschaft der Entität zurück
        /// </summary>
        internal Action<TEntity, int, IDataReader> SetProperty
        {
            get { return this.setProperty; }
        }

        /// <summary>
        ///   Erstellt eine neue Instanz des Typs ProppertyMapping.
        /// </summary>
        /// <typeparam name = "TProperty">Der Typ der Eigenschaft die gemappt werden soll</typeparam>
        /// <param name = "columnName">Der Name der Spalte des Recordsets welche gemappt werden soll</param>
        /// <param name = "getter">Gibt den Wert einer Eigenschaft der Entität zurück.</param>
        /// <param name = "setter">Weist einen aus dem Recordset ausgelesen Wert einer Eigenschaft der Entität zu.</param>
        /// <param name = "allowNullValues">
        ///   Gibt an ob beim Speichern in die Datenbank oder beim Lesen aus der Datenbank Null-Werte erlaubt sind.
        ///   Unterstützt ein CLR-Typ keine null Werte wird default(TPropery) z.B. null oder 0 zurückgegeben.
        /// </param>
        /// <returns>Ein neue Instanz eines PropertyMapping-Objektes</returns>
        public static PropertyMapping<TEntity> Create<TProperty>(string columnName, Getter<TEntity, TProperty> getter, Setter<TEntity, TProperty> setter, bool allowNullValues)
        {
/*
            return new PropertyMapping<TEntity>(
                columnName,
                entity => allowNullValues ? (object)getter(entity) ?? DBNull.Value : getter(entity),
                (entity, ordinal, reader) => setter(entity, allowNullValues && reader.IsDBNull(ordinal) ? default(TProperty) : (TProperty)reader[ordinal]),
                true
                );*/

            return Create(columnName, getter, setter, allowNullValues, true);
        }


        /// <summary>
        ///   Erstellt eine neue Instanz des Typs ProppertyMapping.
        /// </summary>
        /// <typeparam name = "TProperty">Der Typ der Eigenschaft die gemappt werden soll</typeparam>
        /// <param name = "columnName">Der Name der Spalte des Recordsets welche gemappt werden soll</param>
        /// <param name = "getter">Gibt den Wert einer Eigenschaft der Entität zurück.</param>
        /// <param name = "setter">Weist einen aus dem Recordset ausgelesen Wert einer Eigenschaft der Entität zu.</param>
        /// <param name = "allowNullValues">
        ///   Gibt an ob beim Speichern in die Datenbank oder beim Lesen aus der Datenbank Null-Werte erlaubt sind.
        ///   Unterstützt ein CLR-Typ keine null Werte wird default(TPropery) z.B. null oder 0 zurückgegeben.
        /// </param>
        /// <param name = "allowInsert">
        ///   Gibt an, ob Werte in dieser Spalte eingefügt werden dürfen. 
        ///   (Falls nicht explizit erlaubt, dürfen in MSSQL-Identity-Spalten keine Werte eingetragen werden.)
        /// </param>
        /// <returns>Ein neue Instanz eines PropertyMapping-Objektes</returns>
        public static PropertyMapping<TEntity> Create<TProperty>(string columnName, Getter<TEntity, TProperty> getter, Setter<TEntity, TProperty> setter, bool allowNullValues, bool allowInsert)
        {
            return new PropertyMapping<TEntity>(
                columnName,
                entity => allowNullValues ? (object) getter(entity) ?? DBNull.Value : getter(entity),
                (entity, ordinal, reader) => setter(entity, allowNullValues && reader.IsDBNull(ordinal) ? default(TProperty) : (TProperty) reader[ordinal]),
                allowInsert
                );
        }


        /// <summary>
        ///   Gibt den gemappten Wert zurück
        /// </summary>
        /// <param name = "entity">Entität aus der der Wert gelesen werden soll</param>
        internal object GetValue(TEntity entity)
        {
            //addParameter(entity, parameters);
            return this.getProperty(entity);
        }
    }
}