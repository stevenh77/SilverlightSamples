using System;
using System.Collections.Generic;
using Stocks.Common.Models;

namespace Stocks.Common
{
  public interface IConfigurationService
  {
    List<Company> GetCompanies();
    string GetServiceUrl(string symbolCsv);
    string GetServiceUrl(string[] symbols);
  }
}
