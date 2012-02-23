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
namespace Ecw.Libs.Export.Forms.Test
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    internal static class Program
    {
        /// <summary>
        ///   Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var source = new List<Foo>
            {
                new Foo(new DateTime(2003, 3, 30, 3, 37, 26), "Fix", 96.32, null, 1),
                new Foo(new DateTime(2004, 6, 14, 19, 59, 0), "Foxi", 76.15, null, 1),
                new Foo(new DateTime(2003, 12, 1), "Lupinchen", 76.15, false, 0),
                new Foo(null, null, null, null, null),
                new Foo(new DateTime().Add(new TimeSpan(10, 15, 0)), "Lupo", 234.35, false, 0),
                new Foo(new DateTime().Add(new TimeSpan(19, 59, 19)), "Knox", 265, false, 0),
                new Foo(new DateTime(2004, 6, 14, 19, 59, 36, 353), "Eusebia", 76.15, true, 1),
            };

            var exporterForm = ExporterForm.New("Hallo", source, Environment.GetFolderPath(Environment.SpecialFolder.Personal), ExportType.XlsxExport);
            exporterForm.AddColumn("Name", c => c.Name);
            exporterForm.AddColumn("Geburtstag Null", c => c.Datum);
            exporterForm.AddColumn("Geburtstag", c => c.Datum ?? new DateTime());
            exporterForm.AddColumn("Geburtstag Formatiert Null", c => c.Datum, DateTimeFormats.DateTimeSeconds);
            exporterForm.AddColumn("Geburtstag Formatiert", c => c.Datum ?? new DateTime(), DateTimeFormats.DateTimeSecondsMilliseconds);
            exporterForm.AddColumn("Alter Null", c => c.Alter);
            exporterForm.AddColumn("Alter", c => c.Alter ?? new TimeSpan());
            exporterForm.AddColumn("Alter Formatiert Null", c => c.Alter, TimeSpanFormats.HoursMinutesSeconds);
            exporterForm.AddColumn("Alter Formatiert", c => c.Alter ?? new TimeSpan(), TimeSpanFormats.HoursMinutesSecondsMilliseconds);
            exporterForm.AddColumn("Größe Null", c => c.Length);
            exporterForm.AddColumn("Größe", c => c.Length ?? 0);
            exporterForm.AddColumn("Männlich Null", c => c.Maennlich);
            exporterForm.AddColumn("Männlich", c => c.Maennlich ?? false);
            exporterForm.AddColumn("Foo-Objekt", c => c);

            exporterForm.Show();

            Application.Run(exporterForm);
        }

        /*
        private static void Test()
        {
            var exporter = new Exporter<Foo>();        
            var x = exporter.AddTable("Rechnung", e => e.List);
                x.AddColumn("Name", e => e);
                x.AddColumn("Length", e => e.Length);

        }
         */
    }

    public class DateTimeTest
    {
        public DateTime Date;
        public DateTime DateTime;
        public DateTime DateTimeSeconds;
        public DateTime MilliSeconds;
        public DateTime Time;
        public DateTime TimeMilliSeconds;
        public DateTime TimeSeconds;
    }


    public class Foo
    {
        public DateTime? Datum;
        public int? Geschwister;
        public double? Length;
        public bool? Maennlich;
        public string Name;

        public Foo(DateTime? datum, string name, double? length, bool? maennlich, int? geschwister)
        {
            this.Datum = datum;
            this.Name = name;
            this.Length = length;
            this.Maennlich = maennlich;
            this.Geschwister = geschwister;
        }

        public IEnumerable<string> List
        {
            get { return new[] {"Der", "böse", "Wolf"}; }
        }

        public TimeSpan? Alter
        {
            get { return DateTime.Now - this.Datum; }
        }

        public override string ToString()
        {
            return "Ich bin ein Foo-Objekt!";
        }
    }
}