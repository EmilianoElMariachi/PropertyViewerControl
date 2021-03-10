using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PropertyViewerControl.Converters
{
    public class LevelToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var level = (int) value;

            return new Thickness(level * 20, 0,0,0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
