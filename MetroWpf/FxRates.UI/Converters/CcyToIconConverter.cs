using System;
using System.Windows.Data;
using FxRates.UI.Helpers;

namespace FxRates.UI.Converters
{
  public class CcyToIconConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return IconHelper.GetCcyIcon(value.ToString().Substring(5, 3));
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
