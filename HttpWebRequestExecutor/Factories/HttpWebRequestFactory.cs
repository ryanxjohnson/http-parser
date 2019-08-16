using HttpBuilder;
using HttpParser.Models;
using HttpWebRequestExecutor.Interfaces;
using HttpWebRequestExecutor.Lib;

namespace HttpWebRequestExecutor.Factories
{
    public class HttpWebRequestFactory : IHttpWebRequestFactory
    {
        public IHttpWebRequest BuildRequest(ParsedHttpRequest parsed)
        {
            var request = HttpWebRequestBuilder.InitializeWebRequest(parsed);
            return new HttpWebRequestWrapper(request);
        }
    }
}
