using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MetroWpf.Xaml.Converters
{
    public sealed class NumberPositiveToNegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Byte)
                return (Byte)((Byte)value * -1);
            if (value is Int16)
                return (Int16)((Int16)value * -1);
            if (value is Int32)
                return (Int32)value * -1;
            if (value is Int64)
                return (Int64)value * -1;
            if (value is Single)
                return (Single)value * -1f;
            if (value is Double)
                return (Double)value * -1d;
            if (value is Decimal)
                return (Decimal)value * -1m;
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
} ;