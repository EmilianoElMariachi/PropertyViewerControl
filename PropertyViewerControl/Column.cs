using System.Windows;
using System.Windows.Controls;

namespace PropertyViewerControl
{
    public class Column : ColumnDefinition
    {

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(object), typeof(Column), new PropertyMetadata(default(object)));

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

    }
}