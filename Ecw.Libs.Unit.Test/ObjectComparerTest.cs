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
namespace Ecw.Libs.Unit.Test
{
    using System.Collections.Generic;
    using Xunit;

    public class ObjectComparerTest
    {
        public ObjectComparer<Foo> CreateShallowComparer()
        {
            return new ObjectComparer<Foo>()
                .Add("Number", o => o.Number)
                .Add("Name", o => o.Name)
                .Add("Parent", o => o.Parent);
        }

        public IEqualityComparer<Foo> CreateDeepComparer()
        {
            return CreateShallowComparer()
                .Add(o => o.Parent, CreateShallowComparer());
        }


        [Fact]
        public void Name()
        {
            var actual = new Foo
            {
                Number = 2,
                Name = "two"
            };
            var expected = new Foo
            {
                Number = 2,
                Name = "zwei"
            };

            Assert.Throws(typeof (PropertyEqualException), () => Assert.Equal(expected, actual, CreateShallowComparer()));
        }

        [Fact]
        public void OneParentNull()
        {
            var actual = new Foo
            {
                Parent = new Foo(),
            };
            var expected = new Foo();

            Assert.Throws(typeof (PropertyEqualException), () => Assert.Equal(expected, actual, CreateShallowComparer()));
        }

        [Fact]
        public void ParentEqual()
        {
            var actual = new Foo
            {
                Parent = new Foo(),
            };
            var expected = new Foo
            {
                Parent = new Foo(),
            };

            Assert.Equal(expected, actual, CreateShallowComparer());
        }


        [Fact]
        public void ParentNotEqualButShallowComparer()
        {
            var actual = new Foo
            {
                Parent = new Foo {Number = 2},
            };
            var expected = new Foo
            {
                Parent = new Foo {Number = 3},
            };
            Assert.Equal(expected, actual, CreateShallowComparer());
        }

        [Fact]
        public void ParentNotEqual()
        {
            var actual = new Foo
            {
                Parent = new Foo {Number = 2},
            };
            var expected = new Foo
            {
                Parent = new Foo {Number = 3},
            };
            Assert.Equal(expected, actual, CreateShallowComparer());
        }

        [Fact]
        public void ComparerWithComparer()
        {
            var foo = new KeyValuePair<string, IEnumerable<int>>("Hallo", new[] {1, 2, 3});
            var bar = new KeyValuePair<string, IEnumerable<int>>("Hallo", new[] {1, 2, 3, 4});

            var comparer = new ObjectComparer<KeyValuePair<string, IEnumerable<int>>>()
                .Add("Key", x => x.Key)
                .Add(x => x.Value, EqualityComparer<int>.Default.ToSequenceComparer());

            Assert.NotEqual(foo, bar, comparer);
        }

        [Fact]
        public void Test()
        {
            var a = new Foo
            {
                Number = 2,
                Name = "child",
                Parent = new Foo
                {
                    Number = 1,
                    Name = "parent",
                },
            };
        }
    }
}