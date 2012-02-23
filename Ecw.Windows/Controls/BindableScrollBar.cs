// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    //public class BindableScrollBar : ScrollBar
    //{
    //    private ScrollViewer _scrollViewer;
    //    public ScrollViewer BoundScrollViewer { get { return _scrollViewer; } set { _scrollViewer = value; UpdateBindings(); } }

    //    public BindableScrollBar(ScrollViewer scrollViewer, Orientation o)
    //        : base()
    //    {
    //        this.Orientation = o;
    //        BoundScrollViewer = _scrollViewer;
    //    }

    //    public BindableScrollBar()
    //        : base()
    //    {
    //    }

    //    private void UpdateBindings()
    //    {
    //        this.AddHandler(ScrollBar.ScrollEvent, new ScrollEventHandler(OnScroll));
    //        _scrollViewer.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(BoundScrollChanged));
    //        this.Minimum = 0;
    //        if (Orientation == Orientation.Horizontal)
    //        {
    //            this.SetBinding(ScrollBar.MaximumProperty, (new Binding("ScrollableWidth") { Source = _scrollViewer, Mode = BindingMode.OneWay }));
    //            this.SetBinding(ScrollBar.ViewportSizeProperty, (new Binding("ViewportWidth") { Source = _scrollViewer, Mode = BindingMode.OneWay }));
    //        }
    //        else
    //        {
    //            this.SetBinding(ScrollBar.MaximumProperty, (new Binding("ScrollableHeight") { Source = _scrollViewer, Mode = BindingMode.OneWay }));
    //            this.SetBinding(ScrollBar.ViewportSizeProperty, (new Binding("ViewportHeight") { Source = _scrollViewer, Mode = BindingMode.OneWay }));
    //        }
    //        this.LargeChange = 242;
    //        this.SmallChange = 16;
    //    }

    //    public void BoundScrollChanged(object sender, ScrollChangedEventArgs e)
    //    {
    //        switch (this.Orientation)
    //        {
    //            case Orientation.Horizontal:
    //                this.Value = e.HorizontalOffset;
    //                break;
    //            case Orientation.Vertical:
    //                this.Value = e.VerticalOffset;
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //    public void OnScroll(object sender, ScrollEventArgs e)
    //    {
    //        switch (this.Orientation)
    //        {
    //            case Orientation.Horizontal:
    //                this.BoundScrollViewer.ScrollToHorizontalOffset(e.NewValue);
    //                break;
    //            case Orientation.Vertical:
    //                this.BoundScrollViewer.ScrollToVerticalOffset(e.NewValue);
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}
    /// <summary>
    ///   An extended scrollbar that can be bound to an external scrollviewer.
    /// </summary>
    public class BindableScrollBar : ScrollBar
    {
        // Using a DependencyProperty as the backing store for BoundScrollViewer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundScrollViewerProperty =
            DependencyProperty.Register("BoundScrollViewer", typeof (ScrollViewer), typeof (BindableScrollBar), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnBoundScrollViewerPropertyChanged)));


        /// <summary>
        ///   Initializes a new instance of the <see cref = "BindableScrollBar" /> class.
        /// </summary>
        /// <param name = "scrollViewer">The scroll viewer.</param>
        /// <param name = "o">The o.</param>
        public BindableScrollBar(ScrollViewer scrollViewer, Orientation o)
        {
            Orientation = o;
            BoundScrollViewer = scrollViewer;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "BindableScrollBar" /> class.
        /// </summary>
        public BindableScrollBar() {}

        public ScrollViewer BoundScrollViewer
        {
            get { return (ScrollViewer) GetValue(BoundScrollViewerProperty); }
            set { SetValue(BoundScrollViewerProperty, value); }
        }

        private static void OnBoundScrollViewerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as BindableScrollBar;
            if (sender != null && e.NewValue != null)
            {
                sender.UpdateBindings();
            }
        }

        private void UpdateBindings()
        {
            AddHandler(ScrollEvent, new ScrollEventHandler(OnScroll));
            BoundScrollViewer.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(BoundScrollChanged));
            Minimum = 0;
            if (Orientation == Orientation.Horizontal)
            {
                SetBinding(MaximumProperty, (new Binding("ScrollableWidth") {Source = BoundScrollViewer, Mode = BindingMode.OneWay}));
                SetBinding(ViewportSizeProperty, (new Binding("ViewportWidth") {Source = BoundScrollViewer, Mode = BindingMode.OneWay}));
            }
            else
            {
                SetBinding(MaximumProperty, (new Binding("ScrollableHeight") {Source = BoundScrollViewer, Mode = BindingMode.OneWay}));
                SetBinding(ViewportSizeProperty, (new Binding("ViewportHeight") {Source = BoundScrollViewer, Mode = BindingMode.OneWay}));
            }
            LargeChange = 242;
            SmallChange = 16;
        }

        private void BoundScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    Value = e.HorizontalOffset;
                    break;
                case Orientation.Vertical:
                    Value = e.VerticalOffset;
                    break;
                default:
                    break;
            }
        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    BoundScrollViewer.ScrollToHorizontalOffset(e.NewValue);
                    break;
                case Orientation.Vertical:
                    BoundScrollViewer.ScrollToVerticalOffset(e.NewValue);
                    break;
                default:
                    break;
            }
        }
    }
}