using System.Windows;

namespace PVC
{
    public class Column : DependencyObject
    {

        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(double), typeof(Column), new PropertyMetadata(default(double)));

        public double Width
        {
            get => (double) GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

    }
}