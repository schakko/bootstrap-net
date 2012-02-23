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
    using Shared;

    public interface IEntityMapping<TEntity>
    {
        IFactory<TEntity> EntityFactory { get; }

        IEnumerable<PropertyMapping<TEntity>> PropertyMappings { get; }

        /// <summary>
        ///   Fügt der übergeben Parameterliste Parameter inkl. Werten für jede der gemappten Eigenschafen hinzu.
        /// </summary>
        /// <param name = "entity">Die Entität aus der die Werte gelesen werden</param>
        /// <param name = "parameters">Die Parameterliste zu der die Parameter hinzugefügt werden sollen.</param>
        void AddParameters(TEntity entity, SqlParameterCollection parameters);

        /// <summary>
        ///   Fügt der übergeben Parameterliste Parameter inkl. Werten für jede der gemappten Eigenschaften hinzu.
        /// </summary>
        /// <param name = "entity">Die Entität aus der die Werte gelesen werden</param>
        /// <param name = "parameters">Die Parameterliste zu der die Parameter hinzugefügt werden sollen.</param>
        /// <param name = "includeDisallowInsert">
        ///   Gibt an, ob auch Parameter hinzugefügt werden sollen, die mit allowInsert = false markiert sind
        /// </param>
        void AddParameters(TEntity entity, SqlParameterCollection parameters, bool includeDisallowInsert);

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
        IEnumerable<KeyValuePair<string, object>> GetParameter(TEntity entity, bool includeDisallowInsert);

        /// <summary>
        ///   Gibt alle Namen der Gemappten Tabellenspalten zurück
        /// </summary>
        /// <returns>Namen der Tabellenspalten als Sequenz</returns>
        IEnumerable<string> GetColumnNames();

        /// <summary>
        ///   Gibt alle Namen der Gemappten Tabellenspalten zurück
        /// </summary>
        /// <param name = "includeDisallowInsert">
        ///   Gibt an, ob auch Parameter hinzugefügt werden sollen, die mit allowInsert = false markiert sind
        /// </param>
        /// <returns>Namen der Tabellenspalten als Sequenz</returns>
        IEnumerable<string> GetColumnNames(bool includeDisallowInsert);
    }
}