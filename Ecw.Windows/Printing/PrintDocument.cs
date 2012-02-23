// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Printing;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Markup;

    public class PrintDocument
    {
        private readonly IList<IPrintable> pagePopulators = new List<IPrintable>();

        public PrintDocument()
            : this(new Thickness(1)) {}

        public PrintDocument(Thickness margin)
        {
            PageLayout = new PrintLayout
            {
                Margin = margin,
            };
        }

        public PrintLayout PageLayout { get; set; }

        public DataTemplate HeaderTemplate { get; set; }

        public DataTemplate FooterTemplate { get; set; }

        public string PageInfoFormat { get; set; }

        public string PrintInfoFormat { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public Action<FixedDocument> Callback { get; set; }

        public void Add(IPrintable element)
        {
            this.pagePopulators.Add(element);
        }

        public void Add(IEnumerable<IPrintable> elements)
        {
            foreach (var element in elements)
            {
                this.pagePopulators.Add(element);
            }
        }

        public void CreateAsync()
        {
            if (Callback != null)
            {
                Callback(Create());
            }
        }

        public FixedDocument Create()
        {
            var pages = new PrintPages(PageLayout, HeaderTemplate, FooterTemplate, PageInfoFormat, PrintInfoFormat, UserName);

            foreach (var pagePopulator in this.pagePopulators)
            {
                pagePopulator.Populate(ref pages);
            }

            pages.SetPageCount();

            var document = new FixedDocument {Name = Name};

            foreach (var page in pages)
            {
                var documentPage = new FixedPage
                {
                    Width = PageLayout.Size.Width,
                    Height = PageLayout.Size.Height
                };

                page.IsHitTestVisible = false;

                documentPage.Children.Add(page);
                var content = new PageContent();

                ((IAddChild) content).AddChild(documentPage);
                document.Pages.Add(content);
            }

            var printTicket = LocalPrintServer.GetDefaultPrintQueue().DefaultPrintTicket;

            double width = PageLayout.Size.Width;
            double height = PageLayout.Size.Height;

            printTicket.PageOrientation = height > width ? PageOrientation.Portrait : PageOrientation.Landscape;
            printTicket.PageMediaSize = PageLayout.PageMediaSize;
            printTicket.PagesPerSheet = 1;

            document.PrintTicket = printTicket;

            return document;
        }
    }
}