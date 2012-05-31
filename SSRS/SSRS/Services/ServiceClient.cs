using System;
using System.IO;
using System.Net;
using System.Net.Browser;
using System.Windows;
using System.Windows.Browser;
using ServiceStack.Text;

namespace SSRS.Services
{
    public class ServiceClient<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly string _baseUri;

        public bool EnableCookies { get; set; }

        public ServiceClient(string baseUri = "/services")
        {
            // make sure the base uri is set appropriately
            if (!baseUri.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                var source = Application.Current.Host.Source;
                var rootUri = source.AbsoluteUri.Substring(0, source.AbsoluteUri.Length - source.AbsolutePath.Length);
                if (!baseUri.StartsWith("/"))
                    baseUri = "/" + baseUri;
                baseUri = rootUri + baseUri;
            }
            this._baseUri = baseUri;

            // cookies are on by default
            this.EnableCookies = true;
        }

        public void Send(string uri, string method, TRequest data = null)
        {
            // set up the web request
            var webRequest = (HttpWebRequest) WebRequestCreator.ClientHttp.Create(new Uri(_baseUri + uri));
            webRequest.Method = method;

            // if cookies are enabled, pass them in from the browser
            if (this.EnableCookies)
            {
                webRequest.CookieContainer = new CookieContainer();
                webRequest.CookieContainer.SetCookies(new Uri(_baseUri), HtmlPage.Document.Cookies);
            }

            // set the accept header so our response is in json
            webRequest.Accept = "application/json";

            // if we have data to stream, start streaming.  Otherwise we can get the response now.
            if (data != null)
                webRequest.BeginGetRequestStream(RequestCallback, new DataContainer(webRequest, data));
            else
                webRequest.BeginGetResponse(this.ResponseCallback, webRequest);
        }

        private void RequestCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Get the web request stream
                var container = (DataContainer)asyncResult.AsyncState;
                var webRequest = container.WebRequest;
                var stream = webRequest.EndGetRequestStream(asyncResult);

                // set the content type to json
                webRequest.ContentType = "application/json";

                // Serialize using Microsoft serializer
                //var serializer = new DataContractJsonSerializer(typeof(TRequest));
                //serializer.WriteObject(stream, container.Data);
                //stream.Flush();
                //stream.Close();

                // Serialize using ServiceStack serializer
                using (var writer = new StreamWriter(stream))
                {
                    var serializer = new JsonSerializer<TRequest>();
                    serializer.SerializeToWriter(container.Data, writer);
                }

                // now we can get the response
                webRequest.BeginGetResponse(ResponseCallback, webRequest);
            }
            catch (Exception ex)
            {
                // Raise our own event for the error on the UI thread
                var args = new ServiceClientEventArgs<TResponse>(ex);
                Deployment.Current.Dispatcher.BeginInvoke(() => this.OnCompleted(args));
            }

        }

        private void ResponseCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Get the web response
                var webRequest = (HttpWebRequest)asyncResult.AsyncState;
                var webResponse = webRequest.EndGetResponse(asyncResult);

                // Get the web response stream
                var stream = webResponse.GetResponseStream();

                // Deserialize using Microsoft deserializer
                //var serializer = new DataContractJsonSerializer(typeof(TResponse));
                //var response = (TResponse)serializer.ReadObject(stream);

                // Deserialize using ServiceStack deserializer (faster)
                TResponse response;
                using (var reader = new StreamReader(stream))
                {
                    var serializer = new JsonSerializer<TResponse>();
                    response = serializer.DeserializeFromReader(reader);
                }


                // Switch to the UI thread
                var args = new ServiceClientEventArgs<TResponse>(response);
                Deployment.Current.Dispatcher.BeginInvoke(
                    () =>
                    {
                        // if cookies are enabled, pass them back to the browser
                        if (this.EnableCookies && webRequest.CookieContainer != null)
                        {
                            var cookieHeader = webRequest.CookieContainer.GetCookieHeader(new Uri(_baseUri));
                            HtmlPage.Document.Cookies = cookieHeader;
                        }

                        //Raise our own event for the response
                        this.OnCompleted(args);
                    });
            }
            catch (Exception ex)
            {
                // Raise our own event for the error on the UI thread
                var args = new ServiceClientEventArgs<TResponse>(ex);
                Deployment.Current.Dispatcher.BeginInvoke(() => this.OnCompleted(args));
            }
        }

        public void Get(string uri)
        {
            this.Send(uri, "GET");
        }

        public void Post(string uri, TRequest data = null)
        {
            this.Send(uri, "POST", data);
        }

        public void Put(string uri, TRequest data = null)
        {
            this.Send(uri, "PUT", data);
        }

        public void Patch(string uri, TRequest data = null)
        {
            this.Send(uri, "PATCH", data);
        }

        public void Delete(string uri, TRequest data = null)
        {
            this.Send(uri, "DELETE", data);
        }

        public event EventHandler<ServiceClientEventArgs<TResponse>> Completed;

        protected void OnCompleted(ServiceClientEventArgs<TResponse> e)
        {
            var handler = this.Completed;
            if (handler != null)
                handler(this, e);
        }

        private class DataContainer
        {
            public DataContainer(HttpWebRequest webRequest, TRequest data)
            {
                this.WebRequest = webRequest;
                this.Data = data;
            }

            public HttpWebRequest WebRequest { get; private set; }
            public TRequest Data { get; private set; }
        }
    }

    public class ServiceClientEventArgs<TResponse> : EventArgs
        where TResponse : class
    {
        private readonly TResponse _response;
        private readonly Exception _error;

        public ServiceClientEventArgs(TResponse response)
        {
            this._response = response;
        }

        public ServiceClientEventArgs(Exception error)
        {
            this._error = error;
        }

        public TResponse Response
        {
            get { return this._response; }
        }

        public Exception Error
        {
            get { return this._error; }
        }
    }
}
