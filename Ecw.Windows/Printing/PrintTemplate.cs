// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System;
    using System.Windows;

    public class PrintTemplate : PrintableBase
    {
        private readonly object dataContext;
        private readonly DataTemplate template;

        public PrintTemplate(DataTemplate template)
            : this(template, null) {}

        public PrintTemplate(DataTemplate template, object dataContext)
        {
            if(template == null)
            {
                throw new ArgumentNullException("template");
            }
            this.template = template;
            this.dataContext = dataContext;
        }

        public DataTemplate Template
        {
            get { return this.template; }
        }

        public object DataContext
        {
            get { return this.dataContext; }
        }

        public static FrameworkElement Create(DataTemplate template, object dataContext)
        {
            var element = (FrameworkElement) template.LoadContent();
            element.DataContext = dataContext;
            return element;
        }

        public FrameworkElement Create()
        {
            return Create(this.template, this.dataContext);
        }

        public override void Populate(ref PrintPages pages)
        {
            AddElement(ref pages, Create());
        }
    }
}