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
    ///  Dies ist eine Testklasse für "TimeSpanExtensionsTest" und soll
    ///  alle TimeSpanExtensionsTest Komponententests enthalten.
    ///</summary>
    [TestClass]
    public class TimeSpanExtensionsTest
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

        ///<summary>
        ///  Ein Test für "ToString("t")
        ///</summary>
        [TestMethod]
        public void ToString_t_Test()
        {
            var value = new TimeSpan(15, 34, 56);
            string format = "t";
            string expected = "15:34";
            string actual = value.ToString(format);

            Assert.AreEqual(expected, actual);

            value = new TimeSpan(04, 04, 34);
            format = "t";
            expected = "04:04";
            actual = value.ToString(format);

            Assert.AreEqual(expected, actual);
            value = new TimeSpan(00, 01, 59);
            format = "t";
            expected = "00:01";
            actual = value.ToString(format);

            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///  Ein Test für "ToString("t")
        ///</summary>
        [TestMethod]
        public void ToString_T_Test()
        {
            var value = new TimeSpan(15, 34, 56);
            string format = "T";
            string expected = "15:34:56";
            string actual = value.ToString(format);

            Assert.AreEqual(expected, actual);

            value = new TimeSpan(04, 04, 34);
            format = "T";
            expected = "04:04:34";
            actual = value.ToString(format);

            Assert.AreEqual(expected, actual);
            value = new TimeSpan(00, 01, 00);
            format = "T";
            expected = "00:01:00";
            actual = value.ToString(format);

            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///  Ein Test für "ToString("g")
        ///</summary>
        [TestMethod]
        public void ToString_g_Test()
        {
            var value = new TimeSpan(245, 15, 34, 56);
            string format = "g";
            string expected = "245:15:34";
            string actual = value.ToString(format);

            Assert.AreEqual(expected, actual);


            value = new TimeSpan(15, 04, 16);
            format = "g";
            expected = "0:15:04";
            actual = value.ToString(format);

            Assert.AreEqual(expected, actual);


            value = new TimeSpan(1, 00, 00, 00);
            format = "g";
            expected = "1:00:00";
            actual = value.ToString(format);

            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///  Ein Test für "ToString("g")
        ///</summary>
        [TestMethod]
        public void ToString_G_Test()
        {
            var value = new TimeSpan(245, 15, 34, 56);
            string format = "G";
            string expected = "245:15:34:56";
            string actual = value.ToString(format);

            Assert.AreEqual(expected, actual);


            value = new TimeSpan(15, 04, 16);
            format = "G";
            expected = "0:15:04:16";
            actual = value.ToString(format);

            Assert.AreEqual(expected, actual);


            value = new TimeSpan(1, 00, 00, 00);
            format = "G";
            expected = "1:00:00:00";
            actual = value.ToString(format);

            Assert.AreEqual(expected, actual);
        }
    }
}