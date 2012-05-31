//http://stackoverflow.com/questions/397556/wpf-how-to-bind-radiobuttons-to-an-enum

using System;
using System.Windows;
using System.Windows.Data;

namespace MetroWpf.Xaml.Converters
{
  [ValueConversion(typeof(Enum), typeof(Boolean))]
  public class EnumBooleanConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var parameterString = parameter as string;
      if (parameterString == null)
        return DependencyProperty.UnsetValue;

      if (Enum.IsDefined(value.GetType(), value) == false)
        return DependencyProperty.UnsetValue;

      object parameterValue = Enum.Parse(value.GetType(), parameterString);

      return parameterValue.Equals(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var parameterString = parameter as string;
      if (parameterString == null)
        return DependencyProperty.UnsetValue;

      return Enum.Parse(targetType, parameterString);
    }
  }
}