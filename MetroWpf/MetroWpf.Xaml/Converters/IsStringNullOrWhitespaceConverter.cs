
namespace MetroWpf.Xaml.Converters
{
    using System;
    using System.Windows.Data;
    using System.Globalization;

    public class IsStringNullOrWhitespaceConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string)value;
            bool ret = string.IsNullOrEmpty(str);
            if (!ret)
            {
                str = str.Trim();
                ret = string.IsNullOrEmpty(str);
            }

            if ((string)parameter == "Inverse")
            {
                ret = !ret;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
