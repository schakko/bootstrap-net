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
    using System.Collections.Generic;
    using System.Data;
    using Data.EntityMapper;
    using Moq;

    public class Dummy
    {
        public static Foo Foo
        {
            get
            {
                return new Foo
                {
                    ID = 1,
                    Number = 10,
                    Name = "Zehn",
                    Date = new DateTime(2010, 1, 11)
                };
            }
        }

        public static IEnumerable<Foo> Foos
        {
            get
            {
                yield return new Foo
                {
                    ID = 1,
                    Number = 10,
                    Name = "Zehn",
                    Date = new DateTime(2010, 1, 11)
                };
                yield return new Foo
                {
                    ID = 2,
                    Number = 20,
                    Name = "Zwanzig",
                    Date = new DateTime(2020, 2, 12)
                };
                yield return new Foo
                {
                    ID = 3,
                    Number = 30,
                    Name = "Dreizig",
                    Date = new DateTime(2030, 3, 13)
                };
            }
        }

        public static EntityMapping<Foo> EntityMapping
        {
            get
            {
                var mapping = new EntityMapping<Foo>(new EntityFactory<Foo>());
                mapping.Add("ID", f => f.ID, (f, v) => f.ID = v, false, false);
                mapping.Add("Number", f => f.Number, (f, v) => f.Number = v, false);
                mapping.Add("Name", f => f.Name, (f, v) => f.Name = v, true);
                mapping.Add("Date", f => f.Date, (f, v) => f.Date = v, true);
                return mapping;
            }
        }

        // You should pass here a list of test items, their data
        // will be returned by IDataReader
        public static IDataReader CreateDataReader(IList<Foo> data)
        {
            var moq = new Mock<IDataReader>();

            // This var stores current position in 'ojectsToEmulate' list
            int count = -1;

            bool disposed = false;

            moq.Setup(x => x.Read())
                // Return 'True' while list still has an item
                .Returns(() =>
                {
                    if (disposed)
                    {
                        throw new Exception("Reader is disposed");
                    }
                    return count < data.Count - 1;
                })
                // Go to next position
                .Callback(() => count++);

            moq.Setup(r => r.GetOrdinal("ID")).Returns(0);
            moq.Setup(r => r.GetOrdinal("Nummer")).Returns(1);
            moq.Setup(r => r.GetOrdinal("Name")).Returns(2);
            moq.Setup(r => r.GetOrdinal("Date")).Returns(3);

            moq.Setup(r => r[0]).Returns(() => data[count].ID);
            moq.Setup(r => r[1]).Returns(() => data[count].Number);
            moq.Setup(r => r[2]).Returns(() => data[count].Name);
            moq.Setup(r => r[3]).Returns(() => data[count].Date);

            moq.Setup(r => r.Dispose()).Callback(() => disposed = true);

            moq.Setup(r => r.IsClosed).Returns(() => disposed);

            return moq.Object;
        }


        public static IDbCommand CreateDbCommand(IList<Foo> data)
        {
            var moq = new Mock<IDbCommand>();

            IDataReader reader;

            moq.Setup(x => x.ExecuteReader()).Returns(reader = CreateDataReader(data));
            moq.Setup(x => x.Dispose()).Callback(() =>
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            });
            return moq.Object;
        }
    }
}