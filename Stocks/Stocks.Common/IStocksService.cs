using System;

namespace Stocks.Common
{
  public interface IStocksService
  {
    bool IsActive { get; }
    void Start();
    void Stop();
  }
}
 