using System;
namespace Stocks.Common
{
  public interface IWebClientShim : IDisposable
  {
    string DownloadString(string address);
  }
}
