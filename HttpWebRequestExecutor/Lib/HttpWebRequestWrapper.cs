using HttpBuilder.Interfaces;
using System.Net;

namespace HttpBuilder
{
    public class HttpWebRequestWrapper : IHttpWebRequest
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
