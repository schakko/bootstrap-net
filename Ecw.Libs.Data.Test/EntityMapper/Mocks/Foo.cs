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
namespace Ecw.Libs.Data.Test.Mocks
{
    using System;
    using Unit;

    public class EntityBase
    {
        public int ID { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EntityBase && ((EntityBase) obj).ID == ID && obj.GetType() == GetType();
        }

        public bool Equals(EntityBase other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return other.ID == ID && other.GetType() == GetType();
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }

    /// <summary>
    ///   DTRnaudtrane
    /// </summary>
    public class Foo : EntityBase
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }

        public int Compare(Foo x, Foo y)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Foo other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj is Foo && ((Foo) obj).ID == ID;
        }

        public bool Equals(Foo other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            return other.ID == ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }

    public class FooComparer : ObjectComparer<Foo>
    {
        public FooComparer()
        {
            Add("ID", f => f.ID);
            Add("Name", f => f.Name);
            Add("Number", f => f.Number);
        }
    }
}