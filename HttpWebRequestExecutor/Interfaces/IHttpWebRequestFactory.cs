using HttpParser.Models;

namespace HttpWebRequestExecutor.Interfaces
{
    public interface IHttpWebRequestFactory
    {
        IHttpWebRequest BuildRequest(ParsedHttpRequest parsed);
    }
}
