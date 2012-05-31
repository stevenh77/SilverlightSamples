using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MetroWpf.Xaml.Converters
{
    [ValueConversion(typeof(Thickness), typeof(Thickness))]
    public class ThicknessPositiveToNegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Thickness))
            {
                return DependencyProperty.UnsetValue;
            }

            var source = (Thickness)value;

            var parameters = parameter as string;
            if (parameters != null)
            {
                var propertyNames = parameters.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                if (propertyNames.Length > 0)
                {
                    var thickness = new Thickness(0);

                    var changed = false;

                    foreach (var propertyName in propertyNames)
                    {
                        switch (propertyName)
                        {
                            case "Left":
                                thickness.Left = -source.Left;
                                changed = true;
                                break;
                            case "Top":
                                thickness.Top = -source.Top;
                                changed = true;
                                break;
                            case "Right":
                                thickness.Right = -source.Right;
                                changed = true;
                                break;
                            case "Bottom":
                                thickness.Bottom = -source.Bottom;
                                changed = true;
                                break;
                        }
                    }

                    if (changed)
                    {
                        return thickness;
                    }
                }
            }

            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
} ;