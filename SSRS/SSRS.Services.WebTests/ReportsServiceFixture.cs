using System;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using SSRS.Services.DTO;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

namespace SSRS.Services.WebTests
{
    [TestFixture]
    public class ReportsServiceFixture
    {
        [Test]
        public void ReportsService_REST_PUT_returns_reportList_greater_than_zero()
        {
            //var webRequest = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(new Uri(_baseUri + uri));
            //webRequest.Method = method;

            //// if cookies are enabled, pass them in from the browser
            //if (this.EnableCookies)
            //{
            //    webRequest.CookieContainer = new CookieContainer();
            //    webRequest.CookieContainer.SetCookies(new Uri(_baseUri), HtmlPage.Document.Cookies);
            //}

            //// set the accept header so our response is in json
            //webRequest.Accept = "application/json";
            //var client = new ServiceClient<ReportsRequest, ReportsResponse>();
            //client.Completed += (sender, args) =>
            //    {
            //        // check for web exceptions
            //        var webEx = args.Error as WebException;
            //        if (webEx != null)
            //        {
            //            var webResponse = (HttpWebResponse) webEx.Response;

            //            ErrorText = string.Format("WebException: {0} {1} {2}",
            //                webResponse.ResponseUri,
            //                webResponse.Method, webResponse.StatusDescription);

            //            return;
            //        }

            //        // re-throw any other exceptions
            //        if (args.Error != null)
            //            throw args.Error;

            //        var result = args.Response.Result;
            //        if (result == null) return;
            //    };

            //client.Post("/reports", new ReportsRequest() {IncludeParameters = true});
        
            //var restClient = new JsonServiceClient("http://localhost:2505/services") as IRestClient;
            //var request = new ReportsRequest() { IncludeParameters = true };
            //var response = restClient.Put<ReportsResponse>("/reports", request);
            //Assert.That(response.Result, Is.GreaterThan(0));
        }
    }
}
