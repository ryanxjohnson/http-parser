using HttpParser.Models;
using System;

namespace HttpParser
{
    public static class Parser
    {
        public static ParsedHttpRequest ParseRawRequest(string raw, IgnoreHttpParserOptions options = null)
        {
            try
            {
                var lines = SplitLines(raw);

                var requestLine = new RequestLine(lines);
                var requestHeaders = new RequestHeaders(lines);
                requestHeaders.AddHeader("Method", requestLine.Method);
                requestHeaders.AddHeader("HttpVersion", requestLine.HttpVersion);

                var requestCookies = new RequestCookies(lines);
                var requestBody = new RequestBody(requestLine, lines);

                var initial = new ParsedHttpRequest(options)
                {
                    Url = requestLine.Url,
                    Uri = new Uri(requestLine.Url),
                    Headers = requestHeaders.Headers,
                    Cookies = requestCookies.ParsedCookies,
                    RequestBody = requestBody.Body
                };

                initial.ApplyIgnoreOptions();

                return initial;
            }
            catch (CouldNotParseHttpRequestException)
            {
                throw;// new ArgumentException(ex.Message);   
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string[] SplitLines(string raw)
        {
            return raw
                .TrimEnd('\r', '\n')
                .Split(new[] { "\\n", "\n", "\r\n" }, StringSplitOptions.None);
        }
    }
}
