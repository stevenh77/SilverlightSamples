using System;
using System.Web;
using Funq;
using SSRS.Services;
using SSRS.Services.DTO;
using ServiceStack.WebHost.Endpoints;

namespace SSRS.Web
{
    public class AppHost : AppHostBase
    {
        public AppHost() : 
            base("REST Services", 
            typeof(ReportsService).Assembly) {}

        public override void Configure(Container container)
        {
            Routes.Add<ReportsRequest>("reports", "POST");
        }
    }

    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
        }
    }
}