using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MetroWpf.Xaml.Converters
{
    public sealed class PercentToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double))
            {
                return DependencyProperty.UnsetValue;
            }
            return (double)value * 360.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double))
            {
                return DependencyProperty.UnsetValue;
            }
            return (double)value * (1.0 / 360.0);
        }
    }
} ;