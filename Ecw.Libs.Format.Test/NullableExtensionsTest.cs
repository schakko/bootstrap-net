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
namespace Ecw.Libs.Format.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    ///<summary>
    ///  Dies ist eine Testklasse für "NullableExtensionsTest" und soll
    ///  alle NullableExtensionsTest Komponententests enthalten.
    ///</summary>
    [TestClass]
    public class NullableExtensionsTest
    {
        ///<summary>
        ///  Ruft den Testkontext auf, der Informationen
        ///  über und Funktionalität für den aktuellen Testlauf bietet, oder legt diesen fest.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Zusätzliche Testattribute

        // 
        //Sie können beim Verfassen Ihrer Tests die folgenden zusätzlichen Attribute verwenden:
        //
        //Mit ClassInitialize führen Sie Code aus, bevor Sie den ersten Test in der Klasse ausführen.
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Mit ClassCleanup führen Sie Code aus, nachdem alle Tests in einer Klasse ausgeführt wurden.
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        [TestMethod]
        public void NullableDateTimeTest()
        {
            DateTime? x = new DateTime(2000, 12, 31);

            Assert.AreEqual(x.ToString("d"), "31.12.2000");

            DateTime? nil = null;
            string expectet = null;
            string actual = nil.ToString("d");

            Assert.AreEqual(expectet, actual);
        }

        [TestMethod]
        public void NullableTimeSpanTest()
        {
            TimeSpan? x = new TimeSpan(5, 23, 12, 31);

            Assert.AreEqual("23:12", x.ToString("t"));

            DateTime? nil = null;
            string expectet = null;
            string actual = nil.ToString("t");

            Assert.AreEqual(expectet, actual);
        }
    }
}