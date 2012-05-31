using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MetroWpf.Xaml.Converters
{
  public sealed class AngleToCoordinateConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values == null || values.Length < 4)
      {
        return DependencyProperty.UnsetValue;
      }

      var fallbackValue = values[0];
      if (!(values[1] is double) || !(values[2] is double) || !(values[3] is double))
      {
        return fallbackValue;
      }
      var angle = (double)values[1];
      if (angle < 0.0)
      {
        return fallbackValue;
      }
      var areaWidth = (double)values[2];
      var areaHeight = (double)values[3];

      var width = values.Length > 4 ? (values[4] is double ? (double)values[4] : 0.0) : 0.0;
      var height = values.Length > 5 ? (values[5] is double ? (double)values[5] : 0.0) : 0.0;
      var radiusXCoordinate = values.Length > 6 ? (values[5] is double ? (double)values[5] : 0.0) : areaWidth / 2;
      var radiusYCoordinate = values.Length > 7 ? (values[6] is double ? (double)values[6] : 0.0) : areaHeight / 2;

      var length = Math.Max(width, height);
      var radius = Math.Min(areaWidth / 2, areaHeight / 2) - length;

      switch (parameter as string)
      {
        case "X":
        case "x":
          var x = radiusXCoordinate + radius * Math.Cos(angle * Math.PI / 180);
          return x;
        case "Y":
        case "y":
          var y = radiusYCoordinate + radius * Math.Sin(angle * Math.PI / 180);
          return y;
        default:
          return fallbackValue;
      }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      Contract.Ensures(false);
      throw new NotSupportedException();
    }
  }
} ;