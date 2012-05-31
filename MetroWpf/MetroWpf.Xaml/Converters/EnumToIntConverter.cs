using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MetroWpf.Xaml.Converters
{
  public class EnumToIntConverter : MarkupExtension, IValueConverter
  {
    private static EnumToIntConverter _enumToIntConverter = null;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      if (null == _enumToIntConverter)
        _enumToIntConverter = new EnumToIntConverter();

      return _enumToIntConverter;
    }

    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return (int) value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
