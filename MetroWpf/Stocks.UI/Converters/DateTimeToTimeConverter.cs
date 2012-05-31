using System;
using System.Windows.Data;

namespace Stocks.UI.Converters
{
    public class DateTimeToTimeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string dateTimeString = ((DateTime) value).ToString("HH:mm:ss.ffff");

            return dateTimeString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}