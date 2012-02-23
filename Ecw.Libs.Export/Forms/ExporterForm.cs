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
namespace Ecw.Libs.Export.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Properties;

    public static class ExporterForm
    {
        public static ExporterForm<TSource> New<TSource>(string name, IEnumerable<TSource> source)
        {
            return new ExporterForm<TSource>(name, source, null, ExportType.ExcelExport);
        }

        public static ExporterForm<TSource> New<TSource>(string name, IEnumerable<TSource> source, ExportType defaultExportType)
        {
            return new ExporterForm<TSource>(name, source, null, defaultExportType);
        }

        public static ExporterForm<TSource> New<TSource>(string name, IEnumerable<TSource> source, string directory)
        {
            return new ExporterForm<TSource>(name, source, directory, ExportType.CsvExport);
        }

        public static ExporterForm<TSource> New<TSource>(string name, IEnumerable<TSource> source, string defaultPath, ExportType defaultExportType)
        {
            return new ExporterForm<TSource>(name, source, defaultPath, defaultExportType);
        }
    }

    public partial class ExporterForm<TSource> : Form
    {
        private readonly IList<SelectableColumn<TSource, object>> columns = new List<SelectableColumn<TSource, object>>();
        private readonly string directory;
        private readonly string name;
        private readonly IEnumerable<TSource> source;


        internal ExporterForm(string name, IEnumerable<TSource> source, string defaultPath, ExportType defaultExportType)
        {
            InitializeComponent();
            Icon = Resources.export;
            this.source = source;
            this.directory = defaultPath == null ? null : defaultPath.EndsWith(@"\") ? defaultPath : defaultPath + @"\";
            this.name = name;


            // Füllen der Kombinationsfelder
            this.delimiterSelector.DataSource = new[] {new Delimiter(";"), new Delimiter(","), new Delimiter("␣", " "), new Delimiter("TAB", "\n")};
            this.seperatorSelector.DataSource = new[] {new Delimiter(","), new Delimiter(".")};
            this.exportTypeSelector.DataSource = new[]
            {
                ExportType.ExcelExport,
                ExportType.ClipboardExport,
                ExportType.CsvExport,
                ExportType.XlsxExport,
            };

            // Setzen der Standardwerte
            ChangeExportType(defaultExportType);
        }

        private Delimiter SelectedDelimiter
        {
            get { return (Delimiter) this.delimiterSelector.SelectedItem; }
        }

        private Delimiter SelectedSeperator
        {
            get { return (Delimiter) this.seperatorSelector.SelectedItem; }
        }

        private ExportType SelectedExportType
        {
            get { return (ExportType) this.exportTypeSelector.SelectedItem; }
        }


        public void AddColumn<TColumn>(string name, Func<TSource, TColumn> value)
        {
            AddColumn(name, value, true, null);
        }

        public void AddColumn<TColumn>(string name, Func<TSource, TColumn> value, bool isChecked)
        {
            AddColumn(name, value, isChecked, null);
        }

        public void AddColumn<TColumn>(string name, Func<TSource, TColumn> value, Formater<TColumn> format)
        {
            AddColumn(name, value, true, format);
        }

        public void AddColumn<TColumn>(string name, Func<TSource, TColumn> value, bool isChecked, Formater<TColumn> formater)
        {
            // Mittels x => value(x) wird Func<TSource,TColumn> zu Func<TSource> konvertiert
            this.columns.Add(new SelectableColumn<TSource, object>(name, x => value(x), isChecked, (formater ?? new Formater<TColumn>()).Convert()));
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Füllen der Feldauswahllistbox
            foreach (var column in this.columns)
            {
                this.columnSelector.Items.Add(column, column.IsChecked);
            }
        }

        private void ChangeExportType(ExportType type)
        {
            this.exportTypeSelector.SelectedItem = type;
            this.pathInput.Visible = type.HasPath;
            this.pathLabel.Visible = type.HasPath;
            this.pathButton.Visible = type.HasPath;
            this.delimiterLabel.Visible = type.HasDelimiter;
            this.delimiterSelector.Visible = type.HasDelimiter;
            this.seperatorLabel.Visible = type.HasDecimalSeperator;
            this.seperatorSelector.Visible = type.HasDecimalSeperator;

            if (this.directory != null && this.directory != string.Empty)
            {
                this.pathInput.Text = this.directory + this.name + "." + type.DeflaultExtension;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            var rect = new Rectangle(0, ClientRectangle.Height - 100, ClientRectangle.Width, ClientRectangle.Height - 50);
            Invalidate(rect, false);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Brush brush = null;
            Pen pen = null;

            try
            {
                var rectangles = new Rectangle[4];

                brush = new SolidBrush(SystemColors.Control);
                pen = new Pen(new SolidBrush(Color.FromArgb(223, 223, 223)));

                int height = 49;

                int bottom = ClientRectangle.Height - height;
                int width = ClientRectangle.Width;

                e.Graphics.FillRectangle(brush, new Rectangle(0, bottom, width, height));
                bottom--;
                e.Graphics.DrawLine(pen, 0, bottom, width, bottom);
            }
            finally
            {
                if (brush != null)
                {
                    brush.Dispose();
                }
                if (pen != null)
                {
                    pen.Dispose();
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void delimiterSelector_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SelectedDelimiter.Equals(SelectedSeperator) && SelectedSeperator.Equals(new Delimiter(",")))
            {
                this.seperatorSelector.SelectedItem = new Delimiter(".");
            }
        }

        private void seperatorSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedDelimiter.Equals(SelectedSeperator) && SelectedSeperator.Equals(new Delimiter(",")))
            {
                this.delimiterSelector.SelectedItem = new Delimiter(";");
            }
        }

        private void pathButton_Click(object sender, EventArgs e)
        {
            var type = SelectedExportType;
            var dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.CheckPathExists = true;
            dialog.CheckFileExists = false;
            dialog.Filter = type.FilterExtensions;
            dialog.DefaultExt = type.DeflaultExtension;
            dialog.FileName = this.name;
            dialog.InitialDirectory = this.directory;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.pathInput.Text = dialog.FileName;
            }
        }


        /// <summary>
        ///   Gets whether the specified path is a valid absolute file path.
        /// </summary>
        /// <param name = "path">Any path. OK if null or empty.</param>
        public static bool IsValidPath(string path)
        {
            // Unescaped version: ^([a-zA-Z]\:|\\\\[^\/\\:*?"<>|]+\\[^\/\\:*?"<>|]+)(\\[^\/\\:*?"<>|]+)+(\.[^\/\\:*?"<>|]+)$
            const string r = "^([a-zA-Z]\\:|\\\\\\\\[^\\/\\\\:*?\"<>|]+\\\\[^\\/\\\\:*?\"<>|]+)(\\\\[^\\/\\\\:*?\"<>|]+)+(\\.[^\\/\\\\:*?\"<>|]+)$";
            bool isValid = new Regex(r).IsMatch(path);
            if (!isValid)
            {
                MessageBox.Show("Der eingegeben Pfad ist ungültig.", "Exporter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return isValid;
        }


        public bool IsNewOrOverride(string path)
        {
            var info = new FileInfo(path);
            return !info.Exists || MessageBox.Show("Möchten sie die vorhande Datei überschreiben?", "Exporter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string path = this.pathInput.Text;
            var exportType = SelectedExportType;
            if (exportType.Equals(ExportType.ExcelExport) || exportType.Equals(ExportType.ClipboardExport) || IsValidPath(path) && IsNewOrOverride(path))
            {
                try
                {
                    Exporter<TSource> exporter = null;
                    if (SelectedExportType.Equals(ExportType.ClipboardExport))
                    {
                        exporter = new ClipboardExporter<TSource>();
                    }
                    else if (SelectedExportType.Equals(ExportType.CsvExport))
                    {
                        exporter = new CsvExporter<TSource>(this.pathInput.Text, SelectedDelimiter.Value, SelectedSeperator.Value);
                    }
                    else if (SelectedExportType.Equals(ExportType.ExcelExport))
                    {
                        exporter = new ExcelExporter<TSource>();
                    }
                    else //if (SelectedExportType.Equals(ExportType.XlsxExport))
                    {
                        exporter = new XlsxExporter<TSource>(this.name, this.pathInput.Text);
                    }
                    var exportColumns = this.columnSelector.CheckedItems.Cast<Column<TSource, object>>().ToList();

                    exporter.Source = this.source;
                    exporter.AddColumns(exportColumns);
                    exporter.Export();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Export: " + ex.Message, "Exporter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.closeSelector.Checked)
                {
                    Close();
                }
            }
        }

        private void exportTypeSelector_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ChangeExportType(SelectedExportType);
        }
    }
}