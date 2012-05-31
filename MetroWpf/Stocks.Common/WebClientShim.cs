using System.Net;
using System;

namespace Stocks.Common
{
  // Shim to wrap WebClient component to allow the shim to 
  // be mocked or stubbed for unit tests.
  // Favours injection and composition over inheritence
  public class WebClientShim : IWebClientShim
  {
    private WebClient _webClient;

    public WebClientShim(WebClient webClient)
    {
      _webClient = webClient;
    }

    public string DownloadString(string address)
    {
      return _webClient.DownloadString(address).ToString();
    }

    public void Dispose()
    {
      _webClient.Dispose();
    }
  }
}