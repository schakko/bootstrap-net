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
    using System.Data.SqlClient;

    /// <summary>
    ///   Stellt eine EntityMapper bezogene Erweiterungen vr SqlCommand bereit
    /// </summary>
    public static class SqlCommandExtension
    {
        /// <summary>
        ///   Erstellt ein Objekt welches mit Hilfe des übergeben EntityMappings aus einem DataReader Entities ausliest.
        /// </summary>
        /// <typeparam name = "TEntity">Typ der Entität</typeparam>
        /// <param name = "command">SqlCommand mit einer offenen Datanbankverbindung</param>
        /// <param name = "mapping">Das Objekt indem Sql-Parameter auf Eigenschaften der Entität gemappt wurden.</param>
        /// <returns>Ein Objekt zum Erstellen einer Entität aus einem Recordset</returns>
        public static EntityReader<TEntity> ExecuteEntityReader<TEntity>(this SqlCommand command, IEntityMapping<TEntity> mapping)
        {
            return new EntityReader<TEntity>(command.ExecuteReader(), mapping);
        }
    }
}