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
namespace Ecw.Libs.Data.Extensions
{
    using System;
    using System.Data;

    /// <summary>
    ///   Fügt der Schinttstelle IDataRecord Erweitungsmethoden hinzu, 
    ///   die hauptsächlich dazu dienen den Zugriff auf Felder mit dem Inhalt DBNull.Value zu vereinfachen.
    /// </summary>
    public static class DataReaderExtensions
    {
        #region String

        /// <summary>
        ///   Returns a string
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A string, or null if the column's value is NULL</returns>
        public static string GetString(this IDataRecord reader, string name)
        {
            return (string) reader[name];
        }

        /// <summary>
        ///   Returns a string if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A string, or null if the column's value is NULL</returns>
        public static string GetNullableString(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? null : reader.GetString(index);
        }

        /// <summary>
        ///   Returns a string if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The index of the column to read from</param>
        /// <returns>A string, or null if the column's value is NULL</returns>
        public static string GetNullableString(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? null : (string) obj;
        }

        #endregion

        #region Boolean

        /// <summary>
        ///   Returns a bool
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A bool</returns>
        public static bool GetBoolean(this IDataRecord reader, string name)
        {
            return (bool) reader[name];
        }

        /// <summary>
        ///   Returns a bool if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A bool, or null if the column's value is NULL</returns>
        public static bool? GetNullableBoolean(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (bool?) null : reader.GetBoolean(index);
        }


        /// <summary>
        ///   Returns a bool if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A bool, or null if the column's value is NULL</returns>
        public static bool? GetNullableBoolean(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? (bool?) null : (bool) obj;
        }

        #endregion

        #region Byte

        /// <summary>
        ///   Returns a Byte
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A Byte</returns>
        public static Byte GetByte(this IDataRecord reader, string name)
        {
            return (Byte) reader[name];
        }

        /// <summary>
        ///   Returns a Byte if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A Byte, or null if the column's value is NULL</returns>
        public static Byte? GetNullableByte(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (Byte?) null : reader.GetByte(index);
        }

        /// <summary>
        ///   Returns a Byte if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The index of the column to read from</param>
        /// <returns>A Byte, or null if the column's value is NULL</returns>
        public static Byte? GetNullableByte(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? null : (Byte?) obj;
        }

        #endregion

        #region DateTime

        /// <summary>
        ///   Returns a DateTime if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A DateTime, or null if the column's value is NULL</returns>
        public static DateTime GetDateTime(this IDataRecord reader, string name)
        {
            return (DateTime) reader[name];
        }

        /// <summary>
        ///   Returns a DateTime if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A DateTime, or null if the column's value is NULL</returns>
        public static DateTime? GetNullableDateTime(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (DateTime?) null : reader.GetDateTime(index);
        }

        /// <summary>
        ///   Returns a DateTime if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The index of the column to read from</param>
        /// <returns>A DateTime, or null if the column's value is NULL</returns>
        public static DateTime? GetNullableDateTime(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? null : (DateTime?) obj;
        }

        #endregion

        #region Int16

        /// <summary>
        ///   Returns a Int16
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A Int16</returns>
        public static Int16 GetInt16(this IDataRecord reader, string name)
        {
            return (Int16) reader[name];
        }

        /// <summary>
        ///   Returns a Int16 if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A Int16, or null if the column's value is NULL</returns>
        public static Int16? GetNullableInt16(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (Int16?) null : reader.GetInt16(index);
        }

        /// <summary>
        ///   Returns a Int16 if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The index of the column to read from</param>
        /// <returns>A Int16, or null if the column's value is NULL</returns>
        public static Int16? GetNullableInt16(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? null : (Int16?) obj;
        }

        #endregion

        #region Int32

        /// <summary>
        ///   Returns an int
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>An int</returns>
        public static int GetInt32(this IDataRecord reader, string name)
        {
            return (int) reader[name];
        }

        /// <summary>
        ///   Returns an int if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>An int, or null if the column's value is NULL</returns>
        public static int? GetNullableInt32(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (int?) null : reader.GetInt32(index);
        }

        /// <summary>
        ///   Returns an int if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// i
        /// <returns>An int, or null if the column's value is NULL</returns>
        public static int? GetNullableInt32(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? null : (int?) obj;
        }

        #endregion

        #region Int64

        /// <summary>
        ///   Returns an int
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>An int</returns>
        public static long GetInt64(this IDataRecord reader, string name)
        {
            return (long) reader[name];
        }

        /// <summary>
        ///   Returns an int if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>An int, or null if the column's value is NULL</returns>
        public static long? GetNullableInt64(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (long?) null : reader.GetInt64(index);
        }

        /// <summary>
        ///   Returns an int if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// i
        /// <returns>An int, or null if the column's value is NULL</returns>
        public static long? GetNullableInt64(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? (long?) null : (long) obj;
        }

        #endregion

        #region Single

        /// <summary>
        ///   Returns a Single
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        public static Single GetSingle(this IDataRecord reader, int index)
        {
            return reader.GetFloat(index);
        }

        /// <summary>
        ///   Returns a Single
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A Single</returns>
        public static Single GetSingle(this IDataRecord reader, string name)
        {
            return (Single) reader[name];
        }

        /// <summary>
        ///   Returns a Single if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A Single, or null if the column's value is NULL</returns>
        public static Single? GetNullableSingle(this IDataRecord reader, int index)
        {
            var obj = reader[index];
            return obj == DBNull.Value ? null : (Single?) obj;
        }

        /// <summary>
        ///   Returns a Single if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The index of the column to read from</param>
        /// <returns>A Single, or null if the column's value is NULL</returns>
        public static Single? GetNullableSingle(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? null : (Single?) obj;
        }

        #endregion

        #region Double

        /// <summary>
        ///   Returns a double
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A double, or null if the column's value is NULL</returns>
        public static double GetDouble(this IDataRecord reader, string name)
        {
            return (double) reader[name];
        }


        /// <summary>
        ///   Returns a double if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A double, or null if the column's value is NULL</returns>
        public static double? GetNullableDouble(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (double?) null : reader.GetDouble(index);
        }


        /// <summary>
        ///   Returns a double if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The index of the column to read from</param>
        /// <returns>A double, or null if the column's value is NULL</returns>
        public static double? GetNullableDouble(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? null : (double?) obj;
        }

        #endregion

        #region Decimal

        /// <summary>
        ///   Returns a Decimal
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A Decimal</returns>
        public static Decimal GetDecimal(this IDataRecord reader, string name)
        {
            return (Decimal) reader[name];
        }

        /// <summary>
        ///   Returns a Decimal if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A Decimal, or null if the column's value is NULL</returns>
        public static Decimal? GetNullableDecimal(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (Decimal?) null : reader.GetDecimal(index);
        }

        /// <summary>
        ///   Returns a Decimal if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The index of the column to read from</param>
        /// <returns>A Decimal, or null if the column's value is NULL</returns>
        public static Decimal? GetNullableDecimal(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? null : (Decimal?) reader[name];
        }

        #endregion

        #region TimeSpan

        /// <summary>
        ///   Returns a TimeSpan.
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A TimeSpan</returns>
        public static TimeSpan GetTimeSpan(this IDataRecord reader, string name)
        {
            return (TimeSpan) reader[name];
        }

        /// <summary>
        ///   Returns a TimeSpan if one is present, or null if not.
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A TimeSpan, or null if the column's value is NULL</returns>
        public static TimeSpan? GetNullableTimeSpan(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (TimeSpan?) null : ((DateTime) reader[index]).TimeOfDay;
        }

        /// <summary>
        ///   Returns a TimeSpan if one is present, or null if not.
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A TimeSpan, or null if the column's value is NULL</returns>
        public static TimeSpan? GetNullableTimeSpan(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? (TimeSpan?) null : (TimeSpan) obj;
        }

        #endregion

        #region Guid

        /// <summary>
        ///   Returns a Guid
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The name of the column to read from</param>
        /// <returns>A Guid</returns>
        public static Guid GetGuid(this IDataRecord reader, string name)
        {
            return (Guid) reader[name];
        }

        /// <summary>
        ///   Returns a Guid if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "index">The index of the column to read from</param>
        /// <returns>A Guid, or null if the column's value is NULL</returns>
        public static Guid? GetNullableGuid(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (Guid?) null : reader.GetGuid(index);
        }

        /// <summary>
        ///   Returns a Guid if one is present, or null if not
        /// </summary>
        /// <param name = "reader">The IDbReader to read from</param>
        /// <param name = "name">The index of the column to read from</param>
        /// <returns>A Guid, or null if the column's value is NULL</returns>
        public static Guid? GetNullableGuid(this IDataRecord reader, string name)
        {
            var obj = reader[name];
            return obj == DBNull.Value ? null : (Guid?) obj;
        }

        #endregion
    }
}