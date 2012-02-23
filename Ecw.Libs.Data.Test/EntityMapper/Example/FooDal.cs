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
    using Data.EntityMapper;
    using Mocks;
    using Shared;

    public class FooDal : CrudDalBase<Foo>
    {
        public FooDal(string connectionString) : base(connectionString) {}

        protected override string TableName
        {
            get { return "foo"; }
        }

        protected override IFactory<Foo> EntityFactory
        {
            get { return new EntityFactory<Foo>(); }
        }

        protected override void AddMappings(EntityMapping<Foo> mapping)
        {
            mapping.Add("Name", f => f.Name, (f, v) => f.Name = v, false);
        }
    }
}