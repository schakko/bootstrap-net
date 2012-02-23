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
    using System.Collections.Generic;
    using System.Data;
    using Shared;

    /// <summary>
    ///   Erstellt mit Hilfe des übergeben EntityMappings Entities aus einem IDataReader.
    /// </summary>
    /// <typeparam name = "TEntity">Typ des Entites</typeparam>
    public class EntityReader<TEntity> : IDisposable
    {
        private readonly IFactory<TEntity> factory;
        private readonly List<PropertySetter<TEntity>> propertySetters = new List<PropertySetter<TEntity>>(4);
        private readonly IDataReader reader;

        /// <summary>
        /// </summary>
        /// <param name = "reader">Ein geöffneter DataReader aus dem die Werte gelesen werden sollen</param>
        /// <param name = "mapping"></param>
        internal EntityReader(IDataReader reader, IEntityMapping<TEntity> mapping)
        {
            this.reader = reader;
            this.factory = mapping.EntityFactory;

            foreach (var propertyMapping in mapping.PropertyMappings)
            {
                int ordinal = reader.GetOrdinal(propertyMapping.ColumnName);
                Add(new PropertySetter<TEntity>(ordinal, propertyMapping.SetProperty));
            }
        }

        /// <summary>
        ///   Ruft einen Wert ab, der angibt, ob der zugrundeliegenden IDataReader geschlossen ist.
        /// </summary>
        public bool IsClosed
        {
            get { return this.reader.IsClosed; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.reader != null)
            {
                this.reader.Dispose();
            }
        }

        #endregion

        internal void Add(PropertySetter<TEntity> propertySetter)
        {
            this.propertySetters.Add(propertySetter);
        }

        /// <summary>
        ///   Setzt den zugrundeliegenden IDataReader auf den nächsten Datensatz
        /// </summary>
        /// <returns>Ob noch weitere Datensatze vorhanden sind</returns>
        public bool Read()
        {
            return this.reader.Read();
        }

        /// <summary>
        ///   Erstellt eine neue Instanz von TEntity mit den Werten die aus dem 
        ///   übergebenen DataReader ausgelesen wurden
        /// </summary>
        /// <returns>Instanz von TEntity</returns>
        public TEntity CreateEntity()
        {
            var entity = this.factory.Create();
            for (int i = 0; i < this.propertySetters.Count; i++)
            {
                this.propertySetters[i].Set(entity, this.reader);
            }
            return entity;
        }
    }
}