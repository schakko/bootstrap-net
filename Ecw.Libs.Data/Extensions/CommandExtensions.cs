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
    using System.Data.Common;

    public static class CommandExtensions
    {
        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static decimal? ExecuteNullableDecimal(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? (decimal?) null : (decimal) obj;
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static Byte? ExecuteNullableByte(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? (Byte?) null : (Byte) obj;
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static Int16? ExecuteNullableInt16(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? (Int16?) null : (Int16) obj;
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static Int64? ExecuteNullableInt64(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? (Int64?) null : (Int64) obj;
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static Int32? ExecuteNullableInt32(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? (Int32?) null : (Int32) obj;
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static String ExecuteNullableString(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? null : (String) obj;
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static Single? ExecuteNullableSingle(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? (Single?) null : (Single) obj;
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static Double? ExecuteNullableDouble(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? (Double?) null : (Double) obj;
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static DateTime? ExecuteNullableDateTime(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? (DateTime?) null : (DateTime) obj;
        }

        /// <summary>
        ///   Führt die Abfrage aus und gibt die erste Spalte der ersten Zeile im Resultset zurück,
        ///   das durch die Abfrage zurückgegeben wird. Alle anderen Spalten und Zeilen werden ignoriert.
        /// </summary>
        /// <param name = "command"></param>
        /// <returns>
        ///   Wenn der Wert NULL ist, dann wird null zurückgegben, sonst wird gecastet. 
        ///   wenn der Cast misslingt, wird eine Exception geworfen.
        /// </returns>
        public static TimeSpan? ExecuteNullableTimeSpan(this DbCommand command)
        {
            var obj = command.ExecuteScalar();
            return obj == DBNull.Value ? (TimeSpan?) null : ((DateTime) obj).TimeOfDay;
        }
    }
}