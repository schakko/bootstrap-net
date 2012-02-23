namespace Ecw.Windows.Printing
{
    using System.Windows;
    using System.Windows.Media;

    internal class PageMock
    {
        // Es hat den anschein, dass beim ermitteln der Größe des hinzugefügten Elementes
        // der Wert einwenig zu klein ist. Das der Fehler nur bei sehr vielen Elementen auffällt,
        // gehe ich davon aus, dass dieser Fehler absolut und nicht prozentual ist.
        // Daher dieser Faktor.
        private const double magicalAdjustmentFactor = 0.013 * PrintLayout.DotsPerCm;

        internal Size BodySize { get; set; }

        public bool HasElements { get; private set; }

        public double ContentHeight { get; private set; }

        public double SpaceLeft
        {
            get { return BodySize.Height - ContentHeight; }
        }

        public Transform ChildRenderTransform { get; set; }


        public void AddElement(UIElement element)
        {
            double height = element.DesiredSize.Height;
            HasElements = true;

            //Body.Children.Add(element);
            ContentHeight += height + magicalAdjustmentFactor;
        }

        public void RemoveElement(UIElement element)
        {
            double height = element.DesiredSize.Height;
            ContentHeight -= height;
        }

        public bool Fits(Size size)
        {
            return size.Height < SpaceLeft;
        }

        public Size UpdateAndMeasure(FrameworkElement element)
        {
            element.LayoutTransform = ChildRenderTransform;
            element.UpdateLayout();
            element.Measure(new Size(BodySize.Width, double.PositiveInfinity));
            return element.DesiredSize;
        }

        internal void AddPrintElement(PrintTemplate printElement)
        {
            if (printElement != null)
            {
                var element = printElement.Create();
                UpdateAndMeasure(element);
                AddElement(element);
            }
        }

        public Size UpdateAndMeasure(DataTemplate itemTemplate, object itemSource)
        {
            var element = PrintTemplate.Create(itemTemplate, itemSource);
            return UpdateAndMeasure(element);
        }
    }
}