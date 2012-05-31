using System;
using System.Windows.Data;
using FxRates.Common;

namespace FxRates.UI.Converters
{
  public class CcyToDisplayNameConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      string result;
      try
      {
        switch ((Ccy)value)
        {
          case Ccy.AUDtoUSD:
            result = "AUD to USD";
            break;

          case Ccy.EURtoCHF:
            result = "EUR to CHF";
            break;

          case Ccy.EURtoGBP:
            result = "EUR to GBP";
            break;

          case Ccy.EURtoJPY:
            result = "EUR to JPY";
            break;

          case Ccy.EURtoUSD:
            result = "EUR to USD";
            break;

          case Ccy.GBPtoJPY:
            result = "GBP to JPY";
            break;
            
          case Ccy.GBPtoUSD:
            result = "GBP to USD";
            break;

          case Ccy.USDtoCAD:
            result = "USD to CAD";
            break;

          case Ccy.USDtoCHF:
            result = "USD to CHF";
            break;
            
          case Ccy.USDtoJPY:
            result = "USD to JPY";
            break;

          default:
            result = "Unknown";
            break;
        }
      }
      catch
      {
        result = "Unknown";
      }

      return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
