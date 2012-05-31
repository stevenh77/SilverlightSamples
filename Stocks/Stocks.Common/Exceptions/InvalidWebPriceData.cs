using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stocks.Common.Exceptions
{
  [Serializable]
  public class InvalidWebPriceDataException : Exception
  {
    public string WebPriceData { get; set; }
    public InvalidWebPriceDataException() : base ()
    {

    }
    public InvalidWebPriceDataException(string webPriceData) : base(webPriceData)
    {
      WebPriceData = webPriceData;
    }

    public InvalidWebPriceDataException(string webPriceData, Exception innerException)
      : base("Unexpected web price data", innerException)
    {
      WebPriceData = webPriceData;
    }
  }
}
