using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HttpParser.Models
{
    public class RequestLine
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public string HttpVersion { get; set; }

        private string[] validHttpVerbs = { "GET", "POST" };

        public RequestLine(string[] lines)
        {
            var firstLine = lines[0].Split(' ');
            ValidateRequestLine(firstLine);

            SetHttpMethod(firstLine[0]);
            SetUrl(firstLine[1]);
            SetHttpVersion(firstLine[2]);
        }

        private void ValidateRequestLine(string[] firstLine)
        {
            if (firstLine.Length != 3)
                throw new CouldNotParseHttpRequestException("Request Line is not in a valid format");
        }

        private void SetHttpMethod(string method)
        {
            method = method.Trim();

            if (!validHttpVerbs.Contains(method.ToUpper()))
                throw new CouldNotParseHttpRequestException($"Not a valid HTTP Verb: {method}");

            Method = method;
        }

        private void SetUrl(string url)
        {
            if (!IsValidUri(url, out Uri uriResult))
                throw new CouldNotParseHttpRequestException($"URL is not in a valid format: {url}");

            Url = url.Trim();
        }

        private static bool IsValidUri(string url, out Uri uriResult)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private void SetHttpVersion(string version)
        {
            if (!Regex.IsMatch(version, @"HTTP/\d.\d"))
            {
                HttpVersion = "HTTP/1.1";
            }

            HttpVersion = version.Trim();
        }
    }
}
