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
namespace Ecw.Libs.Data.Test
{
    using System;
    using System.Linq;
    using Data.EntityMapper;
    using Mocks;
    using Xunit;
    using Xunit.Extensions;

    public class MyAssert : Assert {}

    public class PropertyMappingTest
    {
        [Theory]
        [InlineData("ID")]
        [InlineData("Name")]
        public void Name(string name)
        {
            var mapping = PropertyMapping<Foo>.Create(name, p => p.ID, (p, v) => p.ID = v, false, false);
            Assert.Equal(name, mapping.ColumnName);
        }

        [Fact]
        public void GetProperty()
        {
            var mapping = PropertyMapping<Foo>.Create("Name", p => p.Name, (p, v) => p.Name = v, false, true);
            Assert.Equal("Zehn", mapping.GetValue(Dummy.Foo));
        }


        [Fact]
        public void GetPropertyNull()
        {
            var mapping = PropertyMapping<Foo>.Create("Text", p => p.Text, (p, v) => p.Text = v, true, true);
            Assert.Equal(DBNull.Value, mapping.GetValue(Dummy.Foo));
        }


        [Fact]
        public void SetProperyString()
        {
            var reader = Dummy.CreateDataReader(Dummy.Foos.ToArray());
            reader.Read();

            var mapping = PropertyMapping<Foo>.Create("Name", p => p.Name, (p, v) => p.Name = v, false, true);

            var actual = new Foo();
            mapping.SetProperty(actual, 2, reader);

            Assert.Equal("Zehn", actual.Name);
        }

        [Fact]
        public void SetProperyNull()
        {
            var reader = Dummy.CreateDataReader(Dummy.Foos.ToArray());
            reader.Read();

            var mapping = PropertyMapping<Foo>.Create("Text", p => p.Text, (p, v) => p.Text = v, true, true);

            var actual = new Foo();
            mapping.SetProperty(actual, 2, reader);

            Assert.Equal(null, actual.Name);
        }
    }
}