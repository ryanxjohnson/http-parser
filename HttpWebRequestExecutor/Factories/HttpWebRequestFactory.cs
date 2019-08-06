using HttpBuilder.Interfaces;
using HttpParser.Models;

namespace HttpBuilder.Factories
{
    public class HttpWebRequestFactory : IHttpWebRequestFactory
    {
        public IHttpWebRequest BuildRequest(ParsedHttpRequest parsed)
        {
            var request = HttpWebRequestBuilder.InitializeRequest(parsed);
            return new FakeHttpWebRequest(request);
        }
    }
}
