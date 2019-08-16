using HttpBuilder.Interfaces;
using System.Net;

namespace HttpBuilder
{
    public class FakeHttpWebRequest : IHttpWebRequest
    {
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
