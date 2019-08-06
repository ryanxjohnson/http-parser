using HttpParser.Models;

namespace HttpBuilder.Interfaces
{
    public interface IHttpWebRequestFactory
    {
        IHttpWebRequest BuildRequest(ParsedHttpRequest parsed);
    }
}
