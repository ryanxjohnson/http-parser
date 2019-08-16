using HttpWebRequestExecutor.Interfaces;
using System.Net;

namespace HttpWebRequestExecutor.Lib
{
    internal class HttpWebRequestWrapper : IHttpWebRequest
    {
        private readonly HttpWebRequest request;

        public HttpWebRequestWrapper(HttpWebRequest request)
        {
            this.request = request;
        }

        public IHttpWebResponse GetResponse()
        {
            return new HttpWebResponseWrapper((HttpWebResponse)request.GetResponse());
        }
    }
}
