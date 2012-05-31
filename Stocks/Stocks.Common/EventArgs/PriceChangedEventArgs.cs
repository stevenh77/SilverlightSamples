using Stocks.Common.Models;

namespace Stocks.Common.Events
{
  public class PriceChangedEventArgs
  {
    public Price Price { get; set; }
  }
}
