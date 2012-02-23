// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    ///   Interaktionslogik für PrintPage.xaml
    /// </summary>
    public partial class PrintPage
    {
        private int currentPage;
        private string printInfoFormat;
        private int totalPages;
        private string userName;

        public PrintPage(PrintLayout pageLayout, Size bodySize, DataTemplate headerTemplate, DataTemplate footerTemplate) //, double headerHeight, double footerHeight)
        {
            InitializeComponent();
            ChildRenderTransform = pageLayout.RenderTransform;
            BodySize = bodySize; //new Size(pageLayout.ContentSize.Width, pageLayout.ContentSize.Height - footerHeight - headerHeight);
            Height = pageLayout.Size.Height - pageLayout.Margin.Bottom - pageLayout.Margin.Top;
            Width = pageLayout.Size.Width - pageLayout.Margin.Left - pageLayout.Margin.Right;
            Margin = pageLayout.Margin;

            if (headerTemplate != null)
            {
                this.Header.Children.Add((UIElement) headerTemplate.LoadContent());
            }
            if (footerTemplate != null)
            {
                this.Footer.Children.Add((UIElement) footerTemplate.LoadContent());
            }
        }

        public string UserName
        {
            get { return this.userName; }
            set
            {
                this.userName = value;
                UpdatePrintInfo();
            }
        }

        public Transform ChildRenderTransform { get; set; }

        public int PageNumber
        {
            get { return this.currentPage; }
            set
            {
                this.currentPage = value;
                UpdatePageInfo();
            }
        }

        public int PageCount
        {
            get { return this.totalPages; }
            set
            {
                this.totalPages = value;
                UpdatePageInfo();
            }
        }

        public string PageInfoFormat { get; set; }

        public string PrintInfoFormat
        {
            get { return this.printInfoFormat; }
            set
            {
                this.printInfoFormat = value;
                UpdatePrintInfo();
            }
        }


        internal Size BodySize { get; set; }

        public bool HasElements { get; private set; }

        public double ContentHeight { get; private set; }

        public Size SpaceLeft
        {
            get { return new Size(BodySize.Width, BodySize.Height - ContentHeight < 0 ? 0 : BodySize.Height - ContentHeight); }
        }

        private void UpdatePageInfo()
        {
            if (PageInfoFormat != null)
            {
                this.PageInfo.Text = string.Format(PageInfoFormat, this.currentPage, this.totalPages);
            }
        }

        private void UpdatePrintInfo()
        {
            if (PrintInfoFormat != null)
            {
                this.PrintInfo.Text = string.Format(PrintInfoFormat, DateTime.Now, UserName);
            }
        }

        internal PageMock CreatePageMock()
        {
            return new PageMock {BodySize = SpaceLeft, ChildRenderTransform = ChildRenderTransform};
        }

        public void AddElement(FrameworkElement element)
        {
            double height = element.DesiredSize.Height;
            //Debug.Print("AddElement \t" + element.GetType() + " Heigth=" + height);
            HasElements = true;
            this.Body.Children.Add(element);
            ContentHeight += height;
        }


        public void UpdateMeasureAndAddElement(FrameworkElement element)
        {
            UpdateAndMeasure(element);
            AddElement(element);
        }

        public bool Fits(Size size)
        {
            return size.Height < SpaceLeft.Height;
        }

        public Size UpdateAndMeasure(FrameworkElement element)
        {
            element.UpdateLayout();
            element.LayoutTransform = ChildRenderTransform;
            element.Measure(new Size(BodySize.Width, double.PositiveInfinity));

            return element.DesiredSize;
        }

        public Size UpdateAndMeasure(ItemsControl element)
        {
            element.BeginInit();
            element.EndInit();
            return UpdateAndMeasure((FrameworkElement) element);
        }
    }
}