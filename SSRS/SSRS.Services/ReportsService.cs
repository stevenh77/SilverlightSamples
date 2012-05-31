using System.Collections.Concurrent;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using SSRS.Services.DTO;
using SSRS.Services.ReportServiceReference;
using ServiceStack.ServiceInterface;

namespace SSRS.Services
{
    public class ReportsService : RestServiceBase<ReportsRequest>
    {
        public override object OnPost(ReportsRequest request)
        {
            var result = new ConcurrentQueue<ReportInfo>();
            var client = new ReportingService2010SoapClient();
            CatalogItem[] catalogItems;

            client.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.None;
            client.ListChildren(null, Settings.ReportPath, true, out catalogItems);

            Parallel.ForEach(catalogItems.Where(r => r.TypeName == "Report"),
                (r) =>
                {
                    ItemParameter[] parameters = null;

                    if (request.IncludeParameters)
                        client.GetItemParameters(null, r.Path, null, false, null, null, out parameters);

                    result.Enqueue(ReportInfo.Create(r.Name, Converter.Convert(parameters), r.Path));
                });

            return new ReportsResponse { Result = result.ToList() };
        }
    }
}
