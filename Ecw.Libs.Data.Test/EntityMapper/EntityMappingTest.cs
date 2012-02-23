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
namespace Ecw.Libs.Data.Test.EntityMapper
{
    using System.Data.SqlClient;
    using System.Linq;
    using Data.EntityMapper;
    using Mocks;
    using Xunit;

    public class EntityMappingTest
    {
        [Fact]
        public void GetColumnNamesTest()
        {
            var mapping = new EntityMapping<Foo>(null);
            mapping.Add("ID", f => f.ID, (f, n) => f.ID = n, true);
            mapping.Add("Name", f => f.Name, (f, n) => f.Name = n, false);

            Assert.Equal("Name", mapping.GetColumnNames().Skip(1).Single());
        }

        [Fact]
        public void AddParametersCountWithInsertTest()
        {
            var mapping = Dummy.EntityMapping;
            var foo = Dummy.Foo;
            var command = new SqlCommand();
            mapping.AddParameters(foo, command.Parameters);
            Assert.Equal(4, command.Parameters.Count);
        }


        [Fact]
        public void AddParametersCountWithoutInsertTest()
        {
            var mapping = Dummy.EntityMapping;
            var foo = Dummy.Foo;
            var command = new SqlCommand();
            mapping.AddParameters(foo, command.Parameters, false);
            Assert.Equal(3, command.Parameters.Count);
        }
    }
}