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
    public class RealSqlTest
    {
/*
        [Fact]
        public void Test()
        {
            IEnumerable<Foo> result;
            string statement = @"
                  select 3 id, 30 number, 'hallo' name, null date
            union select 4   , 40       , 'welt '     , null      ";
            using (var connection = new SqlConnection("Data Source=localhost;Initial Catalog=TLL;Integrated Security=true"))
            {
                //using (
                var command = new SqlCommand(statement, connection);
                //)

                connection.Open();
                var reader = new EntityDataReader<Foo>(command, Dummy.EntityMapping);
                result = reader.Read();
            }

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void SimpleTest() 
        {
            Assert.Equal(2, GetFoos().Count());
        }

        public IEnumerable<Foo> GetFoos()
        {

            string statement = @"
                  select 3 id, 30 number, 'hallo' name, null date
            union select 4   , 40       , 'welt '     , null      ";
            using (var connection = new SqlConnection("Data Source=localhost;Initial Catalog=TLL;Integrated Security=true"))
            {
                using (var command = new SqlCommand(statement, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Foo
                            {
                                ID = (int) reader["ID"]
                            };
                        }
                    }
                }
            }
        }
      */
    }
}