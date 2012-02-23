// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System.Windows;

    public abstract class PrintableBase : IPrintable
    {
        #region IPrintable Members

        public abstract void Populate(ref PrintPages pages);

        #endregion

        protected static void AddElement(ref PrintPages pages, FrameworkElement element)
        {
            var size = pages.Last.UpdateAndMeasure(element);
            bool fits = pages.Last.Fits(size);

            if (pages.Last.HasElements && !fits)
            {
                pages.AddNewPage();
            }
            pages.Last.AddElement(element);
        }
    }
}