using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MetroWpf.Xaml.Converters
{
    [ValueConversion(typeof(GridLength), typeof(double))]
    public sealed class GridLengthToDoubleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var unitType = parameter as string;

            if (!(value is GridLength))
            {
                return DependencyProperty.UnsetValue;
            }

            var length = (GridLength)value;
            switch (unitType)
            {
                case "Auto":
                    return length.IsAuto ? length.Value : DependencyProperty.UnsetValue;
                case "*":
                    return length.IsStar ? length.Value : DependencyProperty.UnsetValue;
                default:
                    return length.IsAbsolute ? length.Value : DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double))
            {
                return new GridLength(0.0, GridUnitType.Auto);
            }

            var unitType = parameter as string;

            switch (unitType)
            {
                case "Auto":
                    return new GridLength(0.0, GridUnitType.Auto);
                case "*":
                    return new GridLength((double)value, GridUnitType.Star);
                default:
                    return new GridLength((double)value, GridUnitType.Pixel);
            }
        }
    }
} ;