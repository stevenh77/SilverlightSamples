using System;
using System.Security.Principal;
using SSRS.Services.DTO;
using SSRS.Services.ReportExecutionServiceReference;
using ServiceStack.ServiceHost;

namespace SSRS.Services
{
    public class ReportExecutionService : IService<ReportExecutionRequest>
    {
        public object Execute(ReportExecutionRequest request)
        {
            string encoding = String.Empty, mimeType = String.Empty, extension = String.Empty;
            Warning[] warnings = null;
            string[] streamIDs = null;
            ServerInfoHeader serverInfoHeader;
            ExecutionInfo executionInfo1, executionInfo2;

            var reportExecutionClient = new ReportExecutionServiceSoapClient();
            reportExecutionClient.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.None;

            var executionHeader = reportExecutionClient.LoadReport(null, request.Name, null, out serverInfoHeader, out executionInfo1);

            reportExecutionClient.SetExecutionParameters(executionHeader, null, Converter.Convert(request.Parameters), "en-us", out executionInfo2);
            byte[] result;
            reportExecutionClient.Render(executionHeader, null, request.Format, null, out result,
                                            out extension, out encoding, out mimeType, out warnings, out streamIDs);

            return new ReportExecutionResponse { Result = result };
        }
    }
}