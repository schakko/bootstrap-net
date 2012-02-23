// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System;
    using System.Windows;

    public class PrintFrameworkElement : PrintableBase
    {
        private readonly Func<FrameworkElement> elementCreator;

        public PrintFrameworkElement(Func<FrameworkElement> elementCreator)
        {
            this.elementCreator = elementCreator;
        }

        public override void Populate(ref PrintPages pages)
        {
            var element = this.elementCreator.Invoke();
            //element.InvalidateVisual();
            AddElement(ref pages, element);
        }
    }
}