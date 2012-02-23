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
namespace Ecw.Libs.Data.Test.EntityMapper.Example
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.EntityMapper;
    using Mocks;
    using Shared;
    using Test.Example;

    public abstract class CrudDalBase<TEntity> : DalBase, ICrudDal<TEntity> where TEntity : EntityBase
    {
        private string deleteAllCommandText;
        private string deleteCommandText;
        private string findAllCommandText;
        private string findByIDCommandText;
        private string insertCommandText;
        private EntityMapping<TEntity> mapping;
        private string updateAllCommandText;
        private string updateCommandText;

        protected CrudDalBase(string connectionString) : base(connectionString) {}

        protected abstract string TableName { get; }

        protected virtual EntityMapping<TEntity> Mapping
        {
            get
            {
                if (this.mapping == null)
                {
                    this.mapping = new EntityMapping<TEntity>(EntityFactory);
                    AddMappings(this.mapping);
                }
                return this.mapping;
            }
        }

        protected virtual string FindAllCommandText
        {
            get { return this.findAllCommandText ?? (this.findAllCommandText = CreateFindAllCommandText(TableName, Mapping.GetColumnNames().ToList()).ToString()); }
        }

        protected virtual string FindByIDCommandText
        {
            get { return this.findByIDCommandText ?? (this.findByIDCommandText = CreateFindByIDCommandText(TableName, Mapping.GetColumnNames().ToList()).ToString()); }
        }

        protected virtual string DeleteCommandText
        {
            get { return this.deleteCommandText ?? (this.deleteCommandText = CreateDeleteCommandText(TableName).ToString()); }
        }

        protected virtual string DeleteAllCommandText
        {
            get { return this.deleteAllCommandText ?? (this.deleteAllCommandText = CreateDeleteAllCommandText(TableName).ToString()); }
        }

        protected virtual string InsertCommandText
        {
            get { return this.insertCommandText ?? (this.insertCommandText = CreateInsertCommandText(TableName, Mapping.GetColumnNames(false).ToList()).ToString()); }
        }

        protected virtual string UpdateCommandText
        {
            get { return this.updateCommandText ?? (this.updateCommandText = CreateUpdateCommandText(TableName, Mapping.GetColumnNames(false).ToList()).ToString()); }
        }

        protected virtual string UpdateAllCommandText
        {
            get { return this.updateAllCommandText ?? (this.updateAllCommandText = CreateUpdateAllCommandText(TableName, Mapping.GetColumnNames(false).ToList()).ToString()); }
        }

        /// <summary>
        ///   Gibt eine Instanz die die Schnittstelle IFactory implementiert zurück.
        /// </summary>
        protected abstract IFactory<TEntity> EntityFactory { get; }

        #region ICrudDal<TEntity> Members

        /// <summary>
        ///   Gibt alle in der Datenbank gespeicherten Objekte der Entität zurück.
        /// </summary>
        /// <returns>Alle persistierten Objekte der Entität</returns>
        public IEnumerable<TEntity> FindAll()
        {
            return CreateCommand(FindAllCommandText)
                .ExecuteReader(Mapping);
        }

        /// <summary>
        ///   Gibt das Objekt mit der entsprechendenen ID zurück.
        /// </summary>
        /// <param name = "id">ID des gesuchten Objektes</param>
        /// <returns>Das gesuchte Objekt</returns>
        public virtual TEntity FindByID(long id)
        {
            return CreateCommand(FindByIDCommandText)
                .AddParameter("ID", id)
                .ExecuteReader(Mapping)
                .SingleOrDefault();
        }

        /// <summary>
        ///   Fügt das übergebene Objekt der Datenbank hinzu.
        /// </summary>
        /// <param name = "entity">Das zu speichende Objekt</param>
        /// <returns>
        ///   Die ID des hinzugefügten Objektes. Wenn kein Objekt hinzugefügt wurde, wird 0 zurück gegeben.
        /// </returns>
        public virtual int Insert(TEntity entity)
        {
            return (int) CreateCommand(InsertCommandText)
                             .AddParameters(this.mapping, entity, false)
                             .ExecuteScalar();
        }

        /// <summary>
        ///   Aktualisiert das übergebene Objekt
        /// </summary>
        /// <param name = "entity">Das zu aktualisierende Objekt</param>
        /// <returns>Die Anzahl der geänderten Datensätze</returns>
        public virtual int Update(TEntity entity)
        {
            return CreateCommand(UpdateCommandText)
                .AddParameters(Mapping, entity, true)
                .ExecuteNonQuery();
        }

        /// <summary>
        ///   Löscht die Entität mit der übergebenen ID aus der Datenbank
        /// </summary>
        /// <param name = "id">ID der Entität</param>
        /// <returns>Anzahl der gelöschten Objekte</returns>
        public virtual int Delete(long id)
        {
            return CreateCommand(DeleteCommandText)
                .AddParameter("ID", id)
                .ExecuteNonQuery();
        }

        #endregion

        /// <summary>
        ///   Fügt einen EntityMapping-Instanz Mappings hinzu. 
        ///   In der Regel sollten alle Spalten der Tabelle bzw. Eigenschaften der Entität gemappt werden.
        /// </summary>
        /// <example>
        ///   <code>
        ///     mapping.Add("Name", entity => entity.Name, (entity, value) => entity.Name = value, false);
        ///   </code>
        /// </example>
        /// <returns></returns>
        protected abstract void AddMappings(EntityMapping<TEntity> mapping);
    }
}