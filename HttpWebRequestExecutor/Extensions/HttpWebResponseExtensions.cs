using System.Net;

namespace HttpWebRequestExecutor.Extensions
{
    public static class HttpWebResponseExtensions
    {
        public static string ResponseString(this HttpWebResponse resp)
        {
            using(var stream = resp.GetResponseStream())
            {
                if (resp.Headers["Content-Encoding"] == "gzip")
                {
                    return stream.DecompressGzipStream().GetStringFromStream();
                }

                return stream.GetStringFromStream();
            }
        }
    }
}
