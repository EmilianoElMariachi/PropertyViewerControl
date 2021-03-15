using System.Windows;

namespace PropertyViewerControl
{
    public class Column : DependencyObject
    {

        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(GridLength), typeof(Column), new PropertyMetadata(default(GridLength), (o, args) =>
            {

            }));

        public GridLength Width
        {
            get => (GridLength)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }


    }
}