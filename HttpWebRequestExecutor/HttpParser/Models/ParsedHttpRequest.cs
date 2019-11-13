using System;
using System.Collections.Generic;
using System.Net;

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
    }
}
