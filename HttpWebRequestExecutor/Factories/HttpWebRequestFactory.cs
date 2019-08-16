using HttpBuilder;
using HttpBuilder.Interfaces;
using HttpParser.Models;

namespace HttpWebRequestExecutor.Factories
{
    public class HttpWebRequestFactory : IHttpWebRequestFactory
    {
        public IHttpWebRequest BuildRequest(ParsedHttpRequest parsed)
        {
            var request = HttpWebRequestBuilder.InitializeRequest(parsed);
            return new HttpWebRequestWrapper(request);
        }
    }
}
