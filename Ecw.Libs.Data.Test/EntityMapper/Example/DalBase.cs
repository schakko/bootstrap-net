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
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using Data.EntityMapper;
    using Shared;

    public abstract class DalBase
    {
        private readonly string conectionString;

        private IFactory<SqlConnection> connectionFactory;

        public DalBase(string connectionString)
        {
            this.conectionString = connectionString;
        }

        protected IFactory<SqlConnection> ConnectionFactory
        {
            get { return this.connectionFactory ?? (this.connectionFactory = new SqlConnectionFactory(this.conectionString)); }
        }

        public FluentCommand CreateCommand(string commandText)
        {
            return new FluentCommand(ConnectionFactory, commandText);
        }

        public StringBuilder CreateInsertCommandText(string tableName, IList<string> columnNames)
        {
            if (columnNames.Count == 0)
            {
                throw new ApplicationException("Es muss mindestens ein Spaltenname übergeben werden");
            }

            var builder = new StringBuilder();
            builder.Append("INSERT INTO ");
            builder.Append(tableName);
            builder.Append(" (");
            AppendParameterList(builder, columnNames);
            builder.Append(")\n");
            builder.Append(" VALUES(");
            AppendParameterList(builder, columnNames, true);
            builder.Append(")\n SELECT isnull(cast(SCOPE_IDENTITY() as bigint),0)");
            return builder;
        }

        public static StringBuilder CreateUpdateCommandText(string tableName, IList<string> columnNames)
        {
            var builder = CreateUpdateAllCommandText(tableName, columnNames);
            builder.Append(" WHERE ID = @ID ");
            return builder;
        }

        public static StringBuilder CreateUpdateAllCommandText(string tableName, IList<string> columnNames)
        {
            if (columnNames.Count == 0)
            {
                throw new ApplicationException("Es muss mindestens ein Spaltenname übergeben werden");
            }

            var builder = new StringBuilder();

            builder.Append("UPDATE ");
            builder.Append(tableName);
            builder.Append(" SET ");
            builder.Append(columnNames[0]);
            builder.Append(" = @");
            builder.Append(columnNames[0]);

            for (int i = 1; i < columnNames.Count; i++)
            {
                builder.Append(", ");
                builder.Append(columnNames[i]);
                builder.Append(" = @");
                builder.Append(columnNames[i]);
            }

            return builder;
        }

        public static StringBuilder CreateFindAllCommandText(string tableName, IList<string> columnNames)
        {
            if (columnNames.Count == 0)
            {
                throw new ApplicationException("Es muss mindestens ein Spaltenname übergeben werden");
            }

            var builder = new StringBuilder();
            builder.Append("SELECT ");
            AppendParameterList(builder, columnNames, false);
            builder.Append(" FROM ");
            builder.Append(tableName);

            return builder;
        }

        public static StringBuilder CreateFindByIDCommandText(string tableName, IList<string> columnNames)
        {
            if (columnNames.Count == 0)
            {
                throw new ApplicationException("Es muss mindestens ein Spaltenname übergeben werden");
            }

            var builder = CreateFindAllCommandText(tableName, columnNames);
            builder.Append(" WHERE ID = @ID ");

            return builder;
        }

        public static StringBuilder CreateDeleteAllCommandText(string tableName)
        {
            var builder = new StringBuilder();
            builder.Append("DELETE FROM ");
            builder.Append(tableName);
            return builder;
        }

        public static StringBuilder CreateDeleteCommandText(string tableName)
        {
            var builder = CreateDeleteAllCommandText(tableName);
            builder.Append(" WHERE ID = @ID ");
            return builder;
        }

        private static void AppendParameterList(StringBuilder builder, IList<string> columnNames)
        {
            AppendParameterList(builder, columnNames, false);
        }

        private static void AppendParameterList(StringBuilder builder, IList<string> columnNames, bool withAt)
        {
            if (withAt)
            {
                builder.Append('@');
            }
            builder.Append(columnNames[0]);

            for (int i = 1; i < columnNames.Count; i++)
            {
                builder.Append(", ");
                if (withAt)
                {
                    builder.Append('@');
                }
                builder.Append(columnNames[i]);
            }
        }

        public static StringBuilder CreateWhere(string parameter, params string[] parameters)
        {
            var builder = new StringBuilder(" WHERE ");
            builder.Append(parameter);
            builder.Append(" = @");
            builder.Append(parameter);
            builder.Append(" ");

            foreach (string param in parameters)
            {
                builder.Append(" AND ");
                builder.Append(param);
                builder.Append(" = @");
                builder.Append(param);
                builder.Append(" ");
            }
            return builder;
        }

        #region Nested type: SqlConnectionFactory

        private class SqlConnectionFactory : IFactory<SqlConnection>
        {
            private readonly string connectionString;

            public SqlConnectionFactory(string connectionString)
            {
                this.connectionString = connectionString;
            }

            #region IFactory<SqlConnection> Members

            public SqlConnection Create()
            {
                return new SqlConnection(this.connectionString);
            }

            #endregion
        }

        #endregion
    }
}