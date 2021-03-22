using System.Windows;

namespace PropertyViewerControl
{
    public class Column : DependencyObject
    {

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(object), typeof(Column), new PropertyMetadata(default(object)));

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(GridLength), typeof(Column), new PropertyMetadata(default(GridLength)));

        public GridLength Width
        {
            get => (GridLength) GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

    }
}