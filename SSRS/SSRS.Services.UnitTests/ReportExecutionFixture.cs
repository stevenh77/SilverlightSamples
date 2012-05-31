using System.IO;
using NUnit.Framework;
using SSRS.Services.DTO;

namespace SSRS.Services.UnitTests
{
    [TestFixture]
    class ReportExecutionFixture
    {
        private const string ReportPath = "/SSRSProject/Stock Price List";

        [Test]
        public void WhenExecute_With_SSRSProject_StockPriceList_And_Excel_ShouldReturnNotNullResult()
        {
            var client = new ReportExecutionService();
            var request = new ReportExecutionRequest { Name = ReportPath, Format = "EXCEL" };
            var parameters = new Parameter[1];
            parameters[0] = new Parameter {Name = "ListPriceReportParameter1", Value = "0"};
            request.Parameters = parameters;           
            
            var response = client.Execute(request) as ReportExecutionResponse;
            
            Assert.NotNull(response);

            string fileName = Path.GetTempPath() + "Stock Price List.xls";
            using (FileStream stream = File.OpenWrite(fileName))
            {
                stream.Write(response.Result, 0, response.Result.Length);
            }
        }

        [Test]
        public void WhenExecute_With_SSRSProject_StockPriceList_And_Pdf_ShouldReturnNotNullResult()
        {
            var client = new ReportExecutionService();

            var request = new ReportExecutionRequest { Name = ReportPath, Format = "PDF" };
            var parameters = new Parameter[1];
            parameters[0] = new Parameter { Name = "ListPriceReportParameter1", Value = "0" };
            request.Parameters = parameters;

            var response = client.Execute(request) as ReportExecutionResponse;

            Assert.NotNull(response);

            string fileName = Path.GetTempPath() + "samplereport.pdf";
            using (FileStream stream = File.OpenWrite(fileName))
            {
                stream.Write(response.Result, 0, response.Result.Length);
            }
        }
    }
}
