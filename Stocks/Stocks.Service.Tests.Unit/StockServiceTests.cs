using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Stocks.Common;
using Xunit;

namespace Stocks.Service.Tests.Unit
{
  public class StockServiceTests
  {
    [Fact]
    public void Service_IsActive_property()
    {
      var file = "../../../Stocks.Service/companyData.json";
      var configurationService = new ConfigurationService(file);
      using (var webClientShim = new WebClientShim(new WebClient()))
      {

        var service = new StocksService(
          configurationService,
          webClientShim);

        Assert.Equal(false, service.IsActive);

        using (var task = Task.Factory.StartNew(() =>
          {
            service.Start();
            Assert.Equal(true, service.IsActive);

            using (var task2 = Task.Factory.StartNew(() => Thread.Sleep(50)))
            { task2.Wait(); }
            service.Stop();
            Assert.Equal(false, service.IsActive);
          }))
        {
          task.Wait();
        }
      }
    }
  }
}
