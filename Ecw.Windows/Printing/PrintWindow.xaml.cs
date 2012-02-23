// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Forms;
    using System.Windows.Threading;
    using System.Windows.Xps.Packaging;
    using ComboBox = System.Windows.Controls.ComboBox;
    using MessageBox = System.Windows.MessageBox;

    /// <summary>
    ///   Interaktionslogik für PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        private readonly PrintDocument creator;
        private readonly IEnumerable<PrintLayout> layouts;

        public PrintWindow(PrintDocument creator, IEnumerable<PrintLayout> layouts)
        {
            InitializeComponent();
            this.creator = creator;
            this.layouts = layouts;

            //StartAsync();
            creator.Callback = DocumentCreated;
        }

        public void Refresh()
        {
            this.documentViewer.Document = null;
            this.CreationMessageLabel.Visibility = Visibility.Visible;
            Action asyncCreate = this.creator.CreateAsync;
            Dispatcher.BeginInvoke(asyncCreate, DispatcherPriority.Normal);
        }

        private void DocumentCreated(FixedDocument document)
        {
            this.CreationMessageLabel.Visibility = Visibility.Hidden;
            this.documentViewer.Document = document;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = @"XPS-Dokumente (*.xps)|*.xps|Alle Dateien (*.*)|*.*";
            dialog.DefaultExt = ".xps";
            dialog.AddExtension = true;
            var result = dialog.ShowDialog();


            // Process save file dialog box results
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                XpsDocument xpsd = null;
                try
                {
                    // Save document
                    string filename = dialog.FileName;

                    var doc = (FixedDocument) this.documentViewer.Document;
                    xpsd = new XpsDocument(filename, FileAccess.Write);
                    var xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
                    xw.Write(doc);
                    xpsd.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Speichern des Dokuments: " + ex.Message);
                }
                finally
                {
                    if (xpsd != null)
                    {
                        xpsd.Close();
                    }
                }
            }
        }

        private void LayoutSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (true)
            {
                this.creator.PageLayout = (PrintLayout) ((ComboBox) sender).SelectedItem;
                Refresh();
            }
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var box = (ComboBox) sender;
            box.ItemsSource = this.layouts;
            box.SelectedIndex = 0;
        }
    }
}