using System;
using System.Text;

namespace Stocks.Common.Models
{
  public class SummaryStats
  {
    public WebRequestStats LastWebRequest { get; set; }
    public int PriceChangeEvents { get; set; }
    public int NumberOfRequests { get; set; }
    public DateTime TimeServiceStarted { get; set; }

    public void Reset()
    {
      LastWebRequest = new WebRequestStats();
      PriceChangeEvents = 0;
      NumberOfRequests = 0;
      TimeServiceStarted = DateTime.Now;
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.AppendLine("Stocks Service Summary Stats:");
      sb.AppendLine(string.Format("  Time service started: {0}", TimeServiceStarted));
      sb.AppendLine(string.Format("  Number of requests: {0}", NumberOfRequests));
      sb.AppendLine(string.Format("  Number of price change events: {0}", PriceChangeEvents));
      sb.AppendLine(string.Format("  Number of symbols sent on last request: {0}", LastWebRequest.SymbolCount));
      sb.AppendLine(string.Format("  Number of prices in last response: {0}", LastWebRequest.PricesDownloaded));
      sb.AppendLine(string.Format("  Time taken for last request: {0}", LastWebRequest.Duration));
      //sb.AppendLine(string.Format("  Response string for last request: {0}", LastWebRequest.Response));
      return sb.ToString();
    }
  }
}