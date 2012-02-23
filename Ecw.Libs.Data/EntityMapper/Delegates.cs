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
    /// <summary>
    ///   Sollte den Wert Eigenschaft des übergeben Entität zurück geben.
    /// </summary>
    /// <typeparam name = "TEntity">Der Typ der Entität</typeparam>
    /// <typeparam name = "TProperty">Der Typ der Eigenschaft</typeparam>
    /// <param name = "entity">Die Entität aus der die Eigenschaft „gewählt“ wird</param>
    /// <returns>Den Wert einer Eigenschaft der Entität</returns>
    public delegate TProperty Getter<TEntity, TProperty>(TEntity entity);

    /// <summary>
    ///   Sollte den übergeben Wert einer Eigenschaft der übergeben Entität zuweisen.
    /// </summary>
    /// <typeparam name = "TEntity">Der Typ der Entität</typeparam>
    /// <typeparam name = "TValue">Der Typ des Wertes</typeparam>
    /// <param name = "entity">Die Entität aus der die Eigenschaft „gewählt“ wird</param>
    /// <param name = "value">Der Wert der der Eigenschaft zugewiesen wird</param>
    public delegate void Setter<TEntity, TValue>(TEntity entity, TValue value);
}