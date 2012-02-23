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
    using System.Data.SqlClient;
    using Shared;

    /// <summary>
    ///   Klasse mit der eine Datenbank-Abfrage teilweise mittels Fluentinterface konfiguriert und ausgeführt werden kann.
    /// </summary>
    public class FluentCommand
    {
        private readonly string commandText;
        private readonly IFactory<SqlConnection> connectionFactory;
        private readonly List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

        /// <summary>
        ///   Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name = "connectionFactory"></param>
        /// <param name = "commandText"></param>
        public FluentCommand(IFactory<SqlConnection> connectionFactory, string commandText)
        {
            this.commandText = commandText;
            this.connectionFactory = connectionFactory;
        }

        private void Populate(SqlParameterCollection collection)
        {
            foreach (var parameter in this.parameters)
            {
                collection.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
            }
        }

        /// <summary>
        ///   Fügt einen SQL-Parameter inkl. Wert hinzu.
        /// </summary>
        /// <param name = "parameterName">Name des Parameters</param>
        /// <param name = "value">Wert des Parameters</param>
        /// <returns>Diese Instanz</returns>
        public FluentCommand AddParameter(string parameterName, object value)
        {
            this.parameters.Add(new KeyValuePair<string, object>(parameterName, value));
            return this;
        }

        /// <summary>
        ///   Fügt der übergeben Parameterliste Parameter inkl. Werten für jede der gemappten Eigenschafen hinzu.
        /// </summary>
        /// <param name = "entity">Die Entität aus der die Werte gelesen werden.</param>
        /// <param name = "mapping">Das Objekt indem Sql-Parameter auf Eigenschaften der Entität gemappt wurden.</param>
        /// <param name = "includeDisallowInsert">
        ///   Gibt an, ob auch Parameter hinzugefügt werden sollen, die mit allowInsert = false markiert sind
        /// </param>
        /// <returns>Diese Instanz</returns>
        public FluentCommand AddParameters<TEntity>(IEntityMapping<TEntity> mapping, TEntity entity, bool includeDisallowInsert)
        {
            this.parameters.AddRange(mapping.GetParameter(entity, includeDisallowInsert));
            return this;
        }

        /// <summary>
        ///   Führt eine SQL-Anweisung aus und gibt die Anzalh der betroffenen Spalten zurück.
        /// </summary>
        /// <returns>Anzahl der betroffenen Spalten</returns>
        public int ExecuteNonQuery()
        {
            return OpenConnection(c => c.ExecuteNonQuery());
        }

        /// <summary>
        ///   Fürt die Abfrage aus und gibt die erste Zeile der ersten Spalte im Resultset zurück,
        ///   das durch die Abfrage zurück gegeben wird.
        ///   Zusätzliche Zeilen oder Spalten werden ignoriert.
        /// </summary>
        /// <returns>Erste Zeile der ersten Spalte im Resultset</returns>
        public object ExecuteScalar()
        {
            return OpenConnection(c => c.ExecuteScalar());
        }

        /// <summary>
        ///   Führt die Abfrage inkl. Reader aus und gibt das Ergebnis mittels der übergebenen
        ///   Funktion als Aufzählung von Objekten zurück.
        /// </summary>
        /// <typeparam name = "TEntity">Typ eines Elements der Aufzählung.</typeparam>
        /// <param name = "entityCreator">
        ///   Transformation von einer Zeile im Recordset zu einem Objekt.
        ///   (Die Funktion liegt innerhalb von der „while(reader.Read())“-Schleife.) 
        /// </param>
        /// <returns>Aufzählung von Objekten</returns>
        /// <example>
        ///   <code>
        ///     ExecuteReader(reader => new Foo { ID = reader.GetInt32("ID", Name = reader.GetString("Name")});
        ///   </code>
        /// </example>
        public IEnumerable<TEntity> ExecuteReader<TEntity>(Func<IDataReader, TEntity> entityCreator)
        {
            using (var connection = this.connectionFactory.Create())
            {
                using (var command = new SqlCommand(this.commandText, connection))
                {
                    Populate(command.Parameters);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return entityCreator(reader);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt das Ergebnis mittels des übergebenen
        ///   Mappers als Aufzählung von Objekten zurück.
        /// </summary>
        /// <typeparam name = "TEntity">Typ eines Elements der Aufzählung.</typeparam>
        /// <param name = "mapping">Das Objekt indem die Spalten des Recordsets auf Eigenschaften der Entiät gemappt wurden.</param>
        /// <returns>Aufzählung von Objekten</returns>
        public IEnumerable<TEntity> ExecuteReader<TEntity>(IEntityMapping<TEntity> mapping)
        {
            using (var connection = this.connectionFactory.Create())
            {
                using (var command = new SqlCommand(this.commandText, connection))
                {
                    Populate(command.Parameters);
                    connection.Open();
                    using (var reader = command.ExecuteEntityReader(mapping))
                    {
                        while (reader.Read())
                        {
                            yield return reader.CreateEntity();
                        }
                    }
                }
            }
        }


        /// <summary>
        ///   Öffnet die Datenbankverbindung und führt die übergebene Funktion aus.
        /// </summary>
        /// <typeparam name = "T">Rückgabetyp</typeparam>
        /// <param name = "function">Funktion die ausgeführt wird.</param>
        /// <returns>Der Returnwert der übergeben Funktion</returns>
        public T OpenConnection<T>(Func<SqlCommand, T> function)
        {
            using (var connection = this.connectionFactory.Create())
            {
                using (var command = new SqlCommand(this.commandText, connection))
                {
                    Populate(command.Parameters);
                    connection.Open();
                    return function(command);
                }
            }
        }
    }
}