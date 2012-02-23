// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System.Collections;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    public class PrintTable : IPrintable
    {
        private readonly Border border;
        private readonly PrintTemplate caption;
        private readonly PrintTemplate columsHeader;
        private readonly DataTemplate itemTemplate;
        private readonly DataTemplate itemsControlTemplate;
        private readonly IEnumerable itemsSource;
        private readonly PrintTemplate tableFooter;
        private readonly PrintTemplate tableHeader;
        private bool first;

        public PrintTable(DataTemplate itemsControlTemplate, IEnumerable itemsSource, PrintTemplate columnsHeader)
            : this(itemsControlTemplate, itemsSource, columnsHeader, null, null, null, null) {}

        public PrintTable(DataTemplate itemsControlTemplate, IEnumerable itemsSource, PrintTemplate columnsHeader, PrintTemplate header)
            : this(itemsControlTemplate, itemsSource, columnsHeader, header, null, null, null) {}

        public PrintTable(DataTemplate itemsControlTemplate, IEnumerable itemsSource, PrintTemplate columnsHeader, PrintTemplate header, PrintTemplate footer)
            : this(itemsControlTemplate, itemsSource, columnsHeader, header, footer, null, null) {}

        public PrintTable(DataTemplate itemsControlTemplate, IEnumerable itemsSource, PrintTemplate columnsHeader, PrintTemplate header, PrintTemplate footer, Border border)
            : this(itemsControlTemplate, itemsSource, columnsHeader, header, footer, border, null) {}

        public PrintTable(DataTemplate itemsControlTemplate, IEnumerable itemsSource, PrintTemplate columnsHeader, PrintTemplate tableHeader, PrintTemplate tableFooter, Border border, PrintTemplate caption)
        {
            var control = (ItemsControl) itemsControlTemplate.LoadContent();
            this.itemTemplate = control.ItemTemplate;
            this.itemsSource = itemsSource;
            this.itemsControlTemplate = itemsControlTemplate;
            this.tableFooter = tableFooter;
            this.tableHeader = tableHeader;
            this.columsHeader = columnsHeader;
            this.border = border;
            this.caption = caption;
        }

        #region IPrintable Members

        public void Populate(ref PrintPages pages)
        {
            this.first = true;
            bool newPage = true;
            var subItemsSource = new ArrayList();
            var mock = pages.Last.CreatePageMock();

            //Debug.Print("Page height: {0}", pages.Last.Height / PrintLayout.DotsPerCm);
            //Debug.Print("Mock height: {0}", mock.BodySize.Height / PrintLayout.DotsPerCm);
            
            mock.AddPrintElement(this.caption);
            mock.AddPrintElement(this.tableHeader);

            double sumsize = 0;

            foreach (var itemSource in this.itemsSource)
            {
                if (newPage)
                {
                    mock.AddPrintElement(this.columsHeader);
                    newPage = false;
                }

                var element = PrintTemplate.Create(this.itemTemplate, itemSource);
                var size = mock.UpdateAndMeasure(element);
                //sumsize += size.Height;
                //Debug.Print("Element size height {0}", size.Height / PrintLayout.DotsPerCm);
                //Debug.Print("Height {0}", mock.BodySize.Height / PrintLayout.DotsPerCm);
                
                if (!mock.Fits(size))
                {
                    

                    if (subItemsSource.Count > 0)
                    {
                        AddToPage(pages.Last, subItemsSource, false);
                    }

                    pages.AddNewPage();
                    mock = pages.Last.CreatePageMock();

                    if (subItemsSource.Count == 0)
                    {
                        mock.AddPrintElement(this.caption);
                        mock.AddPrintElement(this.tableHeader);
                    }
                    subItemsSource = new ArrayList();
                    newPage = true;
                }
               
                subItemsSource.Add(itemSource);
                mock.AddElement(element);
            }


            var tableFooterSize = new Size();
            if (this.tableFooter != null)
            {
                var tableFooterElement = this.tableFooter.Create();
                tableFooterSize = mock.UpdateAndMeasure(tableFooterElement);
            }

            if (mock.Fits(tableFooterSize))
            {
                AddToPage(pages.Last, subItemsSource, true);
            }
            else
            {
                if (subItemsSource.Count > 0) //<- neue Zeile um den Bug zu beheben.
                {
                    /* hier beginnt der Bug*/
                    var last = subItemsSource[subItemsSource.Count - 1];
                    subItemsSource.RemoveAt(subItemsSource.Count - 1);                
                    AddToPage(pages.Last, subItemsSource, false);
                    pages.AddNewPage();
                    AddToPage(pages.Last, new[] {last}, true);
                }
            }

            Debug.Print("Sumsize {0}", sumsize / PrintLayout.DotsPerCm);
        }

        #endregion

        private ItemsControl CreateItemsControl(IEnumerable subItemsSource)
        {
            var result = (ItemsControl) this.itemsControlTemplate.LoadContent();
            result.ItemsSource = subItemsSource;
            return result;
        }

        private void AddToPage(PrintPage page, IEnumerable subItemsSource, bool last)
        {
            if (this.first && this.caption != null)
            {
                page.UpdateMeasureAndAddElement(this.caption.Create());
            }

            if (this.border == null)
            {
                if (this.first && this.tableHeader != null)
                {
                    page.UpdateMeasureAndAddElement(this.tableHeader.Create());
                }

                if (this.columsHeader != null)
                {
                    page.UpdateMeasureAndAddElement(this.columsHeader.Create());
                }
                page.UpdateMeasureAndAddElement(CreateItemsControl(subItemsSource));

                if (last && this.tableFooter != null)
                {
                    page.UpdateMeasureAndAddElement(this.tableFooter.Create());
                }
            }
            else
            {
                var stack = new StackPanel();
                if (this.first && this.tableHeader != null)
                {
                    stack.Children.Add(this.tableHeader.Create());
                }

                if (this.columsHeader != null)
                {
                    stack.Children.Add(this.columsHeader.Create());
                }
                stack.Children.Add((CreateItemsControl(subItemsSource)));

                if (last && this.tableFooter != null)
                {
                    stack.Children.Add(this.tableFooter.Create());
                }

                var actualBorder = GetBorder(this.first, last);
                actualBorder.Child = stack;

                page.UpdateMeasureAndAddElement(actualBorder);
            }
            this.first = false;
        }

        private Border GetBorder(bool first, bool last)
        {
            return new Border
            {
                Background = this.border.Background,
                BorderBrush = this.border.BorderBrush,
                BorderThickness = this.border.BorderThickness,
                CornerRadius = this.border.CornerRadius,
            };
        }
    }
}