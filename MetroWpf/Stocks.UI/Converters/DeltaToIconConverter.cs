using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Stocks.UI.Converters
{
		public class DeltaToIconConverter : IValueConverter
		{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string uri;
			BitmapImage image;
			decimal delta;
			string file = "UNK";
			
			try
			{
		delta = (decimal) value;
		{
		  if (delta > 0)
			file = "UP";
		  else if (delta < 0)
			file = "DOWN";
		  else
			file = "LEVEL";
		}
			}
			finally
			{
				uri = string.Format("../Images/{0}.png", file);
				image = new BitmapImage(new Uri(uri, UriKind.Relative));
			}

			return image;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}