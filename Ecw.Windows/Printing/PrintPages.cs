// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class PrintPages : List<PrintPage>
    {
        private readonly DataTemplate footerTemplate;
        private readonly DataTemplate headerTemplate;

        private readonly string pageInfoFormat;
        private readonly PrintLayout pageLayout;

        private readonly string printInfoFormat;

        private readonly string userName;


        public PrintPages(PrintLayout pageLayout, DataTemplate headerTemplate, DataTemplate footerTemplate, string pageInfoFormat, string printInfoFormat, string userName)
        {
            this.headerTemplate = headerTemplate;
            this.footerTemplate = footerTemplate;
            this.pageInfoFormat = pageInfoFormat;
            this.printInfoFormat = printInfoFormat;
            this.pageLayout = pageLayout;
            this.userName = userName ?? Environment.UserName;

            AddNewPage();

            double infoHeight = GetDesiredSize(Last.Info, pageLayout.ContentSize).Height;
            double headerHeight = GetDesiredSize(Last.Header, pageLayout.ContentSize).Height;
            double footerHeight = GetDesiredSize(Last.Footer, pageLayout.ContentSize).Height;

            //Last.Body.Measure(pageLayout.ContentSize);
            // Last.Body.Arrange(new Rect(new Point(0, 0), pageLayout.ContentSize));
            // Last.BodySize = BodySize = new Size(Last.Body.ActualWidth, Last.Body.ActualHeight);
            //Debug.Print("Bodysize=" + BodySize);
            Last.BodySize = BodySize = new Size(pageLayout.ContentSize.Width, pageLayout.ContentSize.Height - headerHeight - infoHeight - footerHeight);
            // Debug.Print("Bodysize=" + BodySize);
        }

        public Size BodySize { get; private set; }

        public PrintPage Last
        {
            get { return this[Count - 1]; }
        }

        private static Size GetDesiredSize(FrameworkElement element, Size size)
        {
            element.Measure(size);
            return element.DesiredSize;
        }


        public PrintPage AddNewPage()
        {
            var page = new PrintPage(this.pageLayout, BodySize, this.headerTemplate, this.footerTemplate);
            page.PageInfoFormat = this.pageInfoFormat;
            page.PrintInfoFormat = this.printInfoFormat;
            page.UserName = this.userName;
            Add(page);
            page.PageNumber = Count;
            return page;
        }

        public void SetPageCount()
        {
            foreach (var i in this)
            {
                i.PageCount = Count;
            }
        }
    }
}