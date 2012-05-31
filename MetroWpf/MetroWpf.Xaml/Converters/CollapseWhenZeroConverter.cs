namespace MetroWpf.Xaml.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class CollapseWhenZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool collapse = false;

            var i = (int)value;
            if (i == 0)
            {
                collapse = true; 
            }

            if ((string)parameter == "Inverse")
            {
                collapse = !collapse;
            }

            return collapse ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
