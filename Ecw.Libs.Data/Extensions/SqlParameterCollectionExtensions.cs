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
    using System.Data.SqlClient;

    /// <summary>
    ///   Fügt der Klasse SqlParameterCollection Methoden hinzu, 
    ///   die das Hinzufügen von Parametern die null enhalten können vereinfacht
    /// </summary>
    public static class SqlParameterCollectionExtensions
    {
        /// <summary>
        ///   Fügt am Ende der SqlParameterCollection einen Wert hinzu.
        /// </summary>
        /// <param name = "parameters"></param>
        /// <param name = "parameterName">Der Name des Parameters</param>
        /// <param name = "value">
        ///   Der hinzuzufügende Wert. Wenn der Wert null ist, dann wird DBNull.Value hinzugefügt
        /// </param>
        public static void AddWithNullableValue(this SqlParameterCollection parameters, string parameterName, object value)
        {
            parameters.AddWithValue(parameterName, value ?? DBNull.Value);
        }
    }
}