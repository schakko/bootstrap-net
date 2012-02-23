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
namespace Ecw.Libs.Data.Test.Example
{
    using System.Collections.Generic;

    /// <summary>
    ///   Eine Schnittstelle die Standarddatenbankoperantion für eine Entiät bereit stellt.
    /// </summary>
    /// <typeparam name = "TEntity">Typ der Entität</typeparam>
    public interface ICrudDal<TEntity>
    {
        /// <summary>
        ///   Gibt alle in der Datenbank gespeicherten Objekte der Entität zurück.
        /// </summary>
        /// <returns>Alle persistierten Objekte der Entität</returns>
        IEnumerable<TEntity> FindAll();

        /// <summary>
        ///   Gibt das Objekt mit der entsprechendenen ID zurück.
        /// </summary>
        /// <param name = "id">ID des gesuchten Objektes</param>
        /// <returns>Das gesuchte Objekt</returns>
        TEntity FindByID(long id);

        /// <summary>
        ///   Fügt das übergebene Objekt der Datenbank hinzu.
        /// </summary>
        /// <param name = "model">Das zu speichende Objekt</param>
        /// <returns>
        ///   Die ID des hinzugefügten Objektes. Wenn kein Objekt hinzugefügt wurde, wird 0 zurück gegeben.
        /// </returns>
        int Insert(TEntity model);

        /// <summary>
        ///   Aktualisiert das übergebene Objekt
        /// </summary>
        /// <param name = "model">Das zu aktualisierende Objekt</param>
        /// <returns>Die Anzahl der geänderten Datensätze</returns>
        int Update(TEntity model);

        /// <summary>
        ///   Löscht die Entität mit der übergebenen ID aus der Datenbank
        /// </summary>
        /// <param name = "id">ID der Entität</param>
        /// <returns>Anzahl der gelöschten Objekte</returns>
        int Delete(long id);
    }
}