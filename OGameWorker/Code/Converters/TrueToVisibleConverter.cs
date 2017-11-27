using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OGameWorker.Code.Converters
{
    public class TrueToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseValue = value != null && (bool)value;
            return baseValue ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (Visibility)value == Visibility.Visible;
        }
    }

    public class FalseToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseValue = value != null && !(bool)value;
            return baseValue ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value != null && (Visibility)value == Visibility.Visible);
        }
    }
}
