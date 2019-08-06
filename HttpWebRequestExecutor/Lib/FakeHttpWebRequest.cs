using HttpBuilder.Interfaces;
using System.Net;

namespace HttpBuilder
{
    public class FakeHttpWebRequest : IHttpWebRequest
    {
        public string Method { get => request.Method; set => request.Method = value; }

        private readonly HttpWebRequest request;
        public FakeHttpWebRequest(HttpWebRequest request)
        {
            this.request = request;
        }

        public IHttpWebResponse GetResponse()
        {
            return new FakeHttpWebResponse((HttpWebResponse)request.GetResponse());
        }
    }
}
