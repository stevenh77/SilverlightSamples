using Stocks.Common.Core;
using Stocks.Common.Models;
using System;
using Stocks.Common.Exceptions;

namespace Stocks.Common
{
  public class Factory
  {
    public static Price CreatePrice(string webPriceData)
    {
      if (string.IsNullOrEmpty(webPriceData))
        throw new InvalidWebPriceDataException(webPriceData);

      try
      {
        var symbol = webPriceData.Substring(1, webPriceData.IndexOf('"', 2) - 1);

        decimal price = decimal.Parse(webPriceData.Substring(
            webPriceData.Length - webPriceData.Reverse().IndexOf(",")));

        return new Price()
        {
          Symbol = symbol,
          PreviousPrice = price,
          CurrentPrice = price
        };
      }
      catch (Exception e)
      {
        throw new InvalidWebPriceDataException(webPriceData, e);
      }
    }
  }
}
