using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using SilverlightAsyncRestWcf.Services;

namespace SilverlightAsyncRestWcf.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("services", new WebServiceHostFactory(), typeof(CarService)));
        }
    }
}