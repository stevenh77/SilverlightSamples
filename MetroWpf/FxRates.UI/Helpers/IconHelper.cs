using System;
using System.Windows.Media.Imaging;

namespace FxRates.UI.Helpers
{
  class IconHelper
  {
    public static BitmapImage GetCcyIcon(string isocode) 
    {
      string uri;
      BitmapImage image;
      try
      {
        uri = string.Format("../Images/{0}.png", isocode);
        image = new BitmapImage(new Uri(uri, UriKind.Relative));
      }
      catch
      {
        uri = string.Format("../Images/{0}.png", "UNK");
        image = new BitmapImage(new Uri(uri, UriKind.Relative));
      }

      return image;
    }
  }
}
