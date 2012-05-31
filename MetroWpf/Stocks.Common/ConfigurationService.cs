using System;
using System.Collections.Generic;
using Stocks.Common;
using Stocks.Common.Core;
using Stocks.Common.Models;
using Newtonsoft.Json;

namespace Stocks.Common
{
  public class ConfigurationService : IConfigurationService
  {
    private string jsonFilename;
    private string serviceUrlPrefix = "http://finance.yahoo.com/d/quotes.csv?s=";
    private string serviceUrlSuffix = "&f=snl1";

    public ConfigurationService(string filename)
    {
      jsonFilename = filename;
    }

    public List<Company> GetCompanies()
    {
      return new FileSerializer().DeserializeJson<Company>(jsonFilename);
    }

    public string GetServiceUrl(string symbolCsv)
    {
      if (string.IsNullOrEmpty(symbolCsv))
        throw new ArgumentException();

      return string.Concat(serviceUrlPrefix, symbolCsv, serviceUrlSuffix);
    }

    public string GetServiceUrl(string[] symbols)
    {
      if (symbols == null || symbols.Length == 0)
        throw new ArgumentException();
      
      var symbolCsv = string.Join(",", symbols);
      return GetServiceUrl(symbolCsv);
    }
  }
}