using System;
using Stocks.Common.Models;

namespace Stocks.Common.Events
{
  public class PriceChangedEventArgs : EventArgs
  {
    public Price Price { get; set; }
  }
}
