// http://drwpf.com/blog/2009/03/17/tips-and-tricks-making-value-converters-more-accessible-in-markup/
// to use in xaml:  <TextBlock Text="{Binding SomePath, Converter={src:BoolVisibilityConverter}}" />

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Globalization;

namespace MetroWpf.Xaml.Converters
{
  public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
  {
    private static BooleanToVisibilityConverter booleanToVisibilityConverter = null;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      if (null == booleanToVisibilityConverter)
        booleanToVisibilityConverter = new BooleanToVisibilityConverter();

      return booleanToVisibilityConverter;
    }

    public bool IsReversed { get; set; }
    public bool UseHidden { get; set; }

    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var val = System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
      if (this.IsReversed)
      {
        val = !val;
      }
      if (val)
      {
        return Visibility.Visible;
      }
      return this.UseHidden ? Visibility.Hidden : Visibility.Collapsed;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
    
    #endregion
  }
}
