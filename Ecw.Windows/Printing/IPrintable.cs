// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System.Windows.Controls;

    public interface IPrintable
    {
        void Populate(ref PrintPages pages);
    }

    /*
    public class PopulateFromItemsControl : IPopulatePrintPages
    {
        private readonly DataTemplate itemTemplate;
        private readonly IList<object> itemsSource;
        private readonly TriggerCollection triggers;
        private readonly Thickness borderThickness;
        private readonly Brush borderBrush;
        private readonly Thickness margin;
        private readonly int alternationCount;

        private readonly PrintElement columsHeader;
        private readonly PrintElement tableHeader;
        private readonly PrintElement tableFooter;
        private readonly Border border;
        private readonly PrintElement caption;

        public PopulateFromItemsControl(ItemsControl itemsControl, PrintElement columnsHeader, PrintElement tableHeader, PrintElement tableFooter, Border border, PrintElement caption)
        {
            this.columsHeader = columnsHeader;
            this.itemTemplate = itemsControl.ItemTemplate;
            this.itemsSource = itemsControl.ItemsSource.Cast<object>().ToList();
            this.triggers = itemsControl.Triggers;
            this.borderThickness = itemsControl.BorderThickness;
            this.borderBrush = itemsControl.BorderBrush;
            this.margin = itemsControl.Margin;
            this.alternationCount = itemsControl.AlternationCount;

            this.tableFooter = tableFooter;
            this.tableHeader = tableHeader;
            this.border = border;
            this.caption = caption;
        }

        private ItemsControl CreateItemsControl(IEnumerable subItemsSource)
        {
            var result = new ItemsControl
            {
                ItemTemplate = this.itemTemplate,
                BorderBrush = this.borderBrush,
                BorderThickness = this.borderThickness,
                Margin = this.margin,
                AlternationCount = this.alternationCount,
                ItemsSource = subItemsSource
            };
            foreach (var trigger in triggers)
            {
                result.Triggers.Add(trigger);
            }
            return result;
        }
        bool first;

        public void Populate(ref PrintPages pages)
        {
            first = true;
            bool newPage = true;
            ArrayList subItemsSource = new ArrayList();
            PageMock mock = pages.Last.CreatePageMock();

            mock.AddPrintElement(caption);
            mock.AddPrintElement(tableHeader);

            foreach (var itemSource in itemsSource)
            {
                if (newPage)
                {
                    mock.AddPrintElement(columsHeader);
                    newPage = false;
                }

                var element = PrintElement.Create(itemTemplate, itemSource);
                var size = mock.UpdateAndMeasure(element);

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
                        mock.AddPrintElement(caption);
                        mock.AddPrintElement(tableHeader);
                    }
                    subItemsSource = new ArrayList();
                    newPage = true;
                }
                subItemsSource.Add(itemSource);
                mock.AddElement(element);
            }

            var tableFooterElement = tableFooter.Create();
            var tableFooterSize = mock.UpdateAndMeasure(tableFooterElement);

            if (mock.Fits(tableFooterSize))
            {
                AddToPage(pages.Last, subItemsSource, true);
            }
            else
            {
                var rest = new ArrayList();
                // So lange elemente am ende entfernen, bis es passt
                while (subItemsSource.Count > 0 && !mock.Fits(tableFooterSize))
                {
                    rest.Insert(0, subItemsSource[subItemsSource.Count - 1]);
                    subItemsSource.RemoveAt(subItemsSource.Count - 1);
                    var element = PrintElement.Create(itemTemplate, rest[0]);
                    var size = mock.UpdateAndMeasure(element);
                    mock.RemoveElement(element);
                }
                AddToPage(pages.Last, subItemsSource, false);
                AddToPage(pages.Last, rest, true);
            }
        }

        private void AddToPage(PrintPage page, IEnumerable subItemsSource, bool last)
        {
            if (first && caption != null)
            {
                page.AddElement(caption.Create());
            }

            if (border == null)
            {
                if (first && tableHeader != null)
                {
                    page.AddElement(tableHeader.Create());
                }

                if (columsHeader != null)
                {
                    page.AddElement(columsHeader.Create());
                }
                page.AddElement(CreateItemsControl(subItemsSource));

                if (last && tableFooter != null)
                {
                    page.AddElement(tableFooter.Create());
                }
            }
            else
            {
                var stack = new StackPanel();
                if (first && tableHeader != null)
                {
                    stack.Children.Add(tableHeader.Create());
                }

                if (columsHeader != null)
                {
                    stack.Children.Add(columsHeader.Create());
                }
                stack.Children.Add((CreateItemsControl(subItemsSource)));

                if (last && tableFooter != null)
                {
                    stack.Children.Add(tableFooter.Create());
                }

                var actualBorder = GetBorder(first, last);
                actualBorder.Child = stack;

                page.AddElement(actualBorder);
            }
            first = false;
        }



        private Border GetBorder(bool first, bool last)
        {
            return new Border
            {
                Background = this.border.Background,
                BorderBrush = this.border.BorderBrush,
                BorderThickness = this.border.BorderThickness,
                CornerRadius = new CornerRadius
                {
                    TopLeft = first ? border.CornerRadius.TopLeft : 0,
                    TopRight = first ? border.CornerRadius.TopRight : 0,
                    BottomRight = last ? border.CornerRadius.BottomRight : 0,
                    BottomLeft = last ? border.CornerRadius.BottomLeft : 0,
                }
            };
        }
    }

*/

    public class PrintPagebreak : IPrintable
    {
        #region IPrintable Members

        public void Populate(ref PrintPages pages)
        {
            if (pages.Last.HasElements)
            {
                pages.AddNewPage();
            }
        }

        #endregion
    }

    public class PrintSpace : IPrintable
    {
        private readonly double height;
        private readonly double width;


        public PrintSpace(double space)
            : this(space, space) {}

        public PrintSpace(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        #region IPrintable Members

        public void Populate(ref PrintPages pages)
        {
            if (pages.Last.HasElements)
            {
                var element = new Border {Width = this.width, Height = this.height};
                pages.Last.UpdateMeasureAndAddElement(element);
            }
        }

        #endregion
    }


    //public class PopulateManyFromDataTemplate : IPrintable
    //{
    //    private readonly PrintTemplate header;
    //    private readonly DataTemplate itemTemplate;
    //    private readonly IEnumerable itemsSource;

    //    public PopulateManyFromDataTemplate(DataTemplate dataTemplate, IEnumerable dataContexts, PrintTemplate header)
    //    {
    //        this.header = header;
    //        this.itemTemplate = dataTemplate;
    //        this.itemsSource = dataContexts;
    //    }

    //    public void Populate(ref PrintPages pages)
    //    {
    //        bool addHeader = header != null;

    //        foreach (var dataContext in itemsSource)
    //        {
    //            FrameworkElement element = PrintTemplate.Create(this.itemTemplate, dataContext);

    //            Size size = pages.Last.UpdateAndMeasure(element);
    //            bool fits = pages.Last.Fits(size);

    //            if (pages.Last.HasElements && !fits)
    //            {
    //                pages.AddNewPage();
    //                addHeader = true;
    //            }

    //            pages.Last.AddElement(element);
    //            addHeader = false;
    //        }
    //    }
    //}


    //public class PopulateFromDataTemplate : IPopulatePrintPages
    //{
    //    private readonly PrintElement elementCreator;

    //    public PopulateFromDataTemplate(PrintElement elementCreator)
    //    {
    //        this.elementCreator = elementCreator;
    //    }

    //    public void Populate(ref PrintPages pages)
    //    {
    //        FrameworkElement element = elementCreator.Create();

    //        Size size = pages.Last.UpdateAndMeasure(element);
    //        bool fits = pages.Last.Fits(size);
    //        if (pages.Last.HasElements && !fits)
    //        {
    //            pages.AddNewPage();
    //        }
    //        pages.Last.AddElement(element);
    //    }
    //}
}