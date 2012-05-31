
namespace Stocks.Common.Fakes
{
  public class FakeWebClientShim : IWebClientShim
  {
    private string _downloadString;

    public FakeWebClientShim(string downloadString = "")
    {
      _downloadString = downloadString;
    }

    public string DownloadString(string address)
    {
      return _downloadString;
    }

    public void Dispose()
    {
      // do nothing
    }
  }
}
