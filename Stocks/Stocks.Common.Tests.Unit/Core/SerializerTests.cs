using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Stocks.Common.Core;
using Stocks.Common.Models;
using System.IO;

namespace Stocks.Common.Tests.Unit.Core
{
  public class SerializerTests
  {
    [Fact]
    public void Test()
    {
      string filename = "companyData.json";
      List<Company> expected;
      List<Company> actual;

      try
      {
        var file = "../../../Stocks.Service/companyData.json";
        var configurationService = new ConfigurationService(file);
        expected = configurationService.GetCompanies();

        var worker = new FileSerializer();
        worker.SerializeJson(filename, expected);

        actual = worker.DeserializeJson<Company>(filename);
      }
      finally
      {
        if (File.Exists(filename))
          File.Delete(filename);
      }

      Assert.Equal(expected.Count, actual.Count);

      for (int i = 0; i < actual.Count; i++)
      {
        Assert.Equal(expected[i].Symbol, actual[i].Symbol);
        Assert.Equal(expected[i].Name, actual[i].Name);
      }
    }

      //    return new List<Company> 
      //{ 
      //  new Company() { Symbol = "AAPL", Name = "Apple, Inc" },
      //  new Company() { Symbol = "AXP", Name = "American Express Company" },
      //  new Company() { Symbol = "BAC", Name = "Bank of America Corporation" },
      //  new Company() { Symbol = "CSCO", Name = "Cisco Systems, Inc" },
      //  new Company() { Symbol = "DIS", Name = "Walt Disney Company" },
      //     new Company() { Symbol = "GE", Name = "General Electric Company" },
      //  new Company() { Symbol = "GOOG", Name = "Google" },
      //  new Company() { Symbol = "HPQ", Name = "Hewlett-Packard Company" },
      //  new Company() { Symbol = "INTC", Name = "Intel Corporation" },
      //  new Company() { Symbol = "JPM", Name = "JP Morgan Chase & Co" },
      //  new Company() { Symbol = "KO", Name = "Coca-Cola Company" },
      //  new Company() { Symbol = "MSFT", Name = "Microsoft Corporation" },
      //  new Company() { Symbol = "PFE", Name = "Pfizer, Inc." },
      //  new Company() { Symbol = "T", Name = "AT&T Inc." },
      //  new Company() { Symbol = "UTX", Name = "United Technologies Corporation" },
      //  new Company() { Symbol = "VZ", Name = "Verizon Communications Inc." },
      //  new Company() { Symbol = "WMT", Name = "Wal-Mart Stores, Inc." },
      //  new Company() { Symbol = "XOM", Name = "Exxon Mobil Corporation" }
      //};
  }
}
