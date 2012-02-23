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
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Shared;

    /// <summary>
    ///   Mappt eine Entätat auf eine Recordset und umgekehrt
    /// </summary>
    /// <typeparam name = "TEntity">Typ der Entität</typeparam>
    public class EntityMapping<TEntity> : IEntityMapping<TEntity>
    {
        private readonly IFactory<TEntity> entityFactory;
        private readonly List<PropertyMapping<TEntity>> propertyMappings = new List<PropertyMapping<TEntity>>(10);
        //private bool ordinalsSetted;

        /// <summary>
        ///   Erstellt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name = "factory">Eine Schnittstelle welche Instanzen vom Typ TEntity erzeugen kann</param>
        public EntityMapping(IFactory<TEntity> factory)
        {
            this.entityFactory = factory;
        }

        #region IEntityMapping<TEntity> Members

        public IFactory<TEntity> EntityFactory
        {
            get { return this.entityFactory; }
        }

        public IEnumerable<PropertyMapping<TEntity>> PropertyMappings
        {
            get { return this.propertyMappings; }
        }

        /// <summary>
        ///   Fügt der übergeben Parameterliste Parameter inkl. Werten für jede der gemappten Eigenschafen hinzu.
        /// </summary>
        /// <param name = "entity">Die Entität aus der die Werte gelesen werden</param>
        /// <param name = "parameters">Die Parameterliste zu der die Parameter hinzugefügt werden sollen.</param>
        public void AddParameters(TEntity entity, SqlParameterCollection parameters)
        {
            AddParameters(entity, parameters, true);
        }

        /// <summary>
        ///   Fügt der übergeben Parameterliste Parameter inkl. Werten für jede der gemappten Eigenschaften hinzu.
        /// </summary>
        /// <param name = "entity">Die Entität aus der die Werte gelesen werden</param>
        /// <param name = "parameters">Die Parameterliste zu der die Parameter hinzugefügt werden sollen.</param>
        /// <param name = "includeDisallowInsert">
        ///   Gibt an, ob auch Parameter hinzugefügt werden sollen, die mit allowInsert = false markiert sind
        /// </param>
        public void AddParameters(TEntity entity, SqlParameterCollection parameters, bool includeDisallowInsert)
        {
            foreach (var column in this.propertyMappings.Where(c => includeDisallowInsert || c.AllowInsert))
            {
                parameters.AddWithValue(column.ColumnName, column.GetValue(entity));
            }
        }

        ///<summary>
        ///  Gibt eine Aufzählung mit Parameternamen (Key) inkl. Werten für jede der gemappten Eigenschaften (Value) zurück.
        ///</summary>
        ///<param name = "entity">Die Entität aus der die Werte gelesen werden</param>
        ///<param name = "includeDisallowInsert">
        ///  Gibt an, ob auch Parameter hinzugefügt werden sollen, die mit allowInsert = false markiert sind
        ///</param>
        ///<returns>
        ///  Eine Aufzählung mit Parameternamen (Key) inkl. Werten für jede der gemappten Eigenschaften (Value).
        ///</returns>
        public IEnumerable<KeyValuePair<string, object>> GetParameter(TEntity entity, bool includeDisallowInsert)
        {
            return this.propertyMappings.Where(c => includeDisallowInsert || c.AllowInsert).Select(c => new KeyValuePair<string, object>(c.ColumnName, c.GetValue(entity)));
        }

        /// <summary>
        ///   Gibt alle Namen der Gemappten Tabellenspalten zurück
        /// </summary>
        /// <returns>Namen der Tabellenspalten als Sequenz</returns>
        public IEnumerable<string> GetColumnNames()
        {
            return this.propertyMappings.Select(c => c.ColumnName);
        }

        /// <summary>
        ///   Gibt alle Namen der Gemappten Tabellenspalten zurück
        /// </summary>
        /// <param name = "includeDisallowInsert">
        ///   Gibt an, ob auch Parameter hinzugefügt werden sollen, die mit allowInsert = false markiert sind
        /// </param>
        /// <returns>Namen der Tabellenspalten als Sequenz</returns>
        public IEnumerable<string> GetColumnNames(bool includeDisallowInsert)
        {
            if (includeDisallowInsert)
            {
                return this.propertyMappings.Select(c => c.ColumnName);
            }
            else
            {
                return this.propertyMappings.Where(c => c.AllowInsert).Select(c => c.ColumnName);
            }
        }

        #endregion

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
        /// <example>
        ///   <code>
        ///     mapping.Add("Name", entity => entity.Name, (entity, value) => entity.Name = value, false);
        ///   </code>
        /// </example>
        /// <returns>Diese Instanz</returns>
        public EntityMapping<TEntity> Add<TProperty>(string columnName, Getter<TEntity, TProperty> getter, Setter<TEntity, TProperty> setter, bool allowNullValues)
        {
            this.propertyMappings.Add(PropertyMapping<TEntity>.Create(columnName, getter, setter, allowNullValues));
            return this;
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
        /// <returns>Diese Instanz</returns>
        public EntityMapping<TEntity> Add<TProperty>(string columnName, Getter<TEntity, TProperty> getter, Setter<TEntity, TProperty> setter, bool allowNullValues, bool allowInsert)
        {
            this.propertyMappings.Add(PropertyMapping<TEntity>.Create(columnName, getter, setter, allowNullValues, allowInsert));
            return this;
        }
    }
}