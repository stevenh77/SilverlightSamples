namespace Stocks.Common.Models
{
  public class WebRequestStats
  {
    public int Duration { get; set; }
    public int PricesDownloaded { get; set; }
    public string Response { get; set; }
    public string Request { get; set; }
    public int SymbolCount { get; set; }
  }
}
