using System.Collections.Generic;
using Stocks.Common.Models;

namespace Stocks.Common
{
  public interface IStocksService
  {
    IList<Price> GetFullCurrentPrices();
    bool IsRunning { get; }
    void Start();
    void Stop();
  }
}
 