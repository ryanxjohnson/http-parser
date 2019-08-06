using HttpBuilder.Extensions;
using HttpParser.Models;
using System;
using System.Net;

namespace HttpBuilder
{
    public static class HttpWebRequestBuilder
    {
        public static HttpWebRequest InitializeRequest(ParsedHttpRequest parsed, Action<HttpWebRequest> callback = null)
        {
            var req = (HttpWebRequest)WebRequest.Create(parsed.Url);
            req.SetHttpHeaders(parsed.Headers);
            req.AddCookies(parsed);

            callback?.Invoke(req);

            req.SetRequestData(parsed.RequestBody);

            return req;
        }

        private static void AddCookies(this HttpWebRequest request, ParsedHttpRequest parsed)
        {
            if (parsed.CookieContainer != null)
            {
                request.CookieContainer = parsed.CookieContainer;
                return;
            }

            if (parsed.Cookies != null)
            {
                request.SetHttpCookies(parsed.Cookies, parsed.Uri);
            }
        }
    }
}
