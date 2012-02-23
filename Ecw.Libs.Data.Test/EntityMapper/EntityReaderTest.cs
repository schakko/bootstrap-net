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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.EntityMapper;
    using Mocks;
    using Xunit;

    public class EntityReaderTest
    {
        [Fact]
        public void CountTest()
        {
            var reader = Dummy.CreateDataReader(Dummy.Foos.ToArray());
            var mapping = Dummy.EntityMapping;
            var test = new EntityReader<Foo>(reader, mapping);
            int count = 0;

            while (test.Read())
            {
                count++;
            }

            Assert.Equal(3, count);
        }

        [Fact]
        public void DesposeTest()
        {
            var test = new EntityReader<Foo>(Dummy.CreateDataReader(Dummy.Foos.ToArray()), Dummy.EntityMapping);
            var x = (IDisposable) test;
            bool wasClosed = test.IsClosed;
            x.Dispose();
            Assert.True(test.IsClosed && !wasClosed);
        }

        [Fact]
        public void CreateEntity()
        {
            var expected = Dummy.Foos.ToArray();
            var reader = Dummy.CreateDataReader(expected);
            var mapping = Dummy.EntityMapping;
            var test = new EntityReader<Foo>(reader, mapping);

            var actual = new List<Foo>();

            while (test.Read())
            {
                actual.Add(test.CreateEntity());
            }

            Assert.Equal(expected, actual.ToArray());
        }
    }
}