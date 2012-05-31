using NUnit.Framework;
using SSRS.Services;
using SSRS.Services.DTO;

namespace SSRS.Tests
{
    [TestFixture]
    public class ReportsServiceFixture
    {
        [Test]
        public void WhenExecute_IncludingParameters_ShouldReturnNotNullResult()
        {
            var service = new ReportsService(); 
            var request = new ReportsRequest() { IncludeParameters = true };
            var response = service.OnGet(request) as ReportsResponse;
            Assert.NotNull(response);
        }

        [Test]
        public void WhenExecute_ExcludingParameters_ShouldReturnNotNullResult()
        {
            var service = new ReportsService();
            var request = new ReportsRequest() { IncludeParameters = false };
            var response = service.OnGet(request) as ReportsResponse;
            Assert.NotNull(response);
        }
    }
}
