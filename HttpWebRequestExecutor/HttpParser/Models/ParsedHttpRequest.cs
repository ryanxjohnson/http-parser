using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HttpParser.Models
{
    public class ParsedHttpRequest
    {
        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Cookies { get; set; }
        public string RequestBody { get; set; }
        public Uri Uri { get; set; }
        public CookieContainer CookieContainer { get; set; }

        private IgnoreHttpParserOptions ignoreHttpParserOptions;
        public ParsedHttpRequest(IgnoreHttpParserOptions options = null)
        {
            ignoreHttpParserOptions = options;
        }

        public void ApplyIgnoreOptions()
        {
            if (ignoreHttpParserOptions == null) return;

            if (ignoreHttpParserOptions.IgnoreUrl)
            {
                Url = null;
            }
            if (ignoreHttpParserOptions.IgnoreHeaders)
            {
                Headers = null;
            }
            if (ignoreHttpParserOptions.IgnoreCookies)
            {
                Cookies = null;
                CookieContainer = null;
            }
            if (ignoreHttpParserOptions.IgnoreRequestBody)
            {
                RequestBody = null;
            }
        }

        public override string ToString()
        {
            var method = Headers["Method"];
            var version = Headers["HttpVersion"];

            var sb = new StringBuilder($"{method} {Url} {version}{Environment.NewLine}");

            var headersToIgnore = new List<string> { "Method", "HttpVersion" };
            foreach(var header in Headers)
            {
                if (headersToIgnore.Contains(header.Key)) continue;
                sb.Append($"{header.Key}: {header.Value}{Environment.NewLine}");
            }

            if (Cookies?.Count > 0)
            {
                var cookies = string.Join(";", Cookies
                    .Select(cookie => $" {cookie.Key}={cookie.Value};"))
                    .TrimEnd(';');

                sb.Append($"Cookie:{cookies}{Environment.NewLine}");
            }
            
            if (method == "POST")
            {
                sb.Append(Environment.NewLine);
                sb.Append(RequestBody);
            }

            return sb.ToString().Trim();
        }
    }
}
