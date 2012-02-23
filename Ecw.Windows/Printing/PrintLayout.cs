// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System.Printing;
    using System.Windows;
    using System.Windows.Media;

    public class PrintLayout
    {
        public const double DotsPerCm = 96/2.54;
        public string Name { get; set; }

        public Size Size { get; set; }

        public Thickness Margin { get; set; }

        public Size ContentSize
        {
            get { return new Size(Size.Width - Margin.Left - Margin.Right, Size.Height - Margin.Top - Margin.Bottom); }
        }

        public Transform RenderTransform { get; set; }

        public PageMediaSize PageMediaSize { get; set; }

        public static PrintLayout DinA5Portrait
        {
            get
            {
                return new PrintLayout
                {
                    Size = CreateMetricSize(new Size(14.8, 21)),
                    PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA5),
                    Name = "DinA5 Hochformat",
                };
            }
        }

        public static PrintLayout DinA5Landscape
        {
            get
            {
                return new PrintLayout
                {
                    Size = CreateMetricSize(new Size(21, 14.8)),
                    PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA5),
                    Name = "DinA5 Querformat",
                };
            }
        }

        public static PrintLayout DinA4Portrait
        {
            get
            {
                return new PrintLayout
                {
                    Size = new Size(8.27*96, 11.69*96),
                    PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4),
                    Name = "DinA4 Hochformat",
                };
            }
        }

        public static PrintLayout DinA4Landscape
        {
            get
            {
                return new PrintLayout
                {
                    Size = new Size(11.69*96, 8.27*96),
                    PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4),
                    Name = "DinA4 Querformat",
                };
            }
        }

        public static PrintLayout DinA3Portrait
        {
            get
            {
                return new PrintLayout
                {
                    Size = CreateMetricSize(new Size(29.7, 42)),
                    PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA3),
                    Name = "DinA3 Hochformat",
                };
            }
        }

        public static PrintLayout DinA3Landscape
        {
            get
            {
                return new PrintLayout
                {
                    Size = CreateMetricSize(new Size(42, 29.7)),
                    PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA3),
                    Name = "DinA3 Querformat",
                };
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public static Thickness CreateCmMargin(Thickness margin)
        {
            return new Thickness(margin.Left*DotsPerCm, margin.Top*DotsPerCm, margin.Right*DotsPerCm, margin.Bottom*DotsPerCm);
        }

        public static Size CreateMetricSize(Size size)
        {
            return new Size(size.Width*DotsPerCm, size.Height*DotsPerCm);
        }
    }
}