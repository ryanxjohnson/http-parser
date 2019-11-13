using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HttpBuilder.Extensions
{
    internal static class HttpWebRequestExtensions
    {
        public static void SetHttpHeaders(this HttpWebRequest request, Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                try
                {
                    request.SetHttpHeader(header.Key, header.Value);
                }
                catch(Exception)
                {
                    Console.WriteLine($"Could not set HTTP header {header.Key}: {header.Value}");
                }
            }
        }

        public static void SetHttpCookies(this HttpWebRequest request, Dictionary<string, string> cookies, Uri uri)
        {
            if (cookies == null) return;

            request.CookieContainer = new CookieContainer();

            foreach(var cookie in cookies)
            {
                request.CookieContainer.Add(uri, new Cookie(cookie.Key, cookie.Value));
            }
        }

        public static void SetRequestData(this HttpWebRequest request, string requestData)
        {
            if (string.IsNullOrEmpty(requestData)) return;

            if (request.Method == "POST")
            {
                request.AddRequestData(requestData);
            }
        }

        private static HttpWebRequest SetHttpHeader(this HttpWebRequest request, string key, string value)
        {
            switch (key.ToLower())
            {
                case "method":
                    request.Method = value;
                    break;
                case "accept":
                    request.Accept = value;
                    break;
                case "connection":
                    request.KeepAlive = value.ToLower() == "keep-alive";
                    //req.Connection = value;
                    // throws System.ArgumentException : "Keep-Alive and Close may not be set using this property."
                    break;
                case "contenttype":
                case "content-type":
                    request.ContentType = value;
                    break;
                case "content-length":
                case "contentlength":
                    request.ContentLength = Convert.ToInt64(value);
                    break;
                case "date":
                    request.Date = Convert.ToDateTime(value);
                    break;
                case "expect":
                    if (value == "100-continue")
                        break;
                    request.Expect = value;
                    break;
                case "host":
                    request.Host = value;
                    break;
                case "httpversion":
                    var version = Convert.ToString(value).Split('/')[1];
                    request.ProtocolVersion = Version.Parse(version);
                    break;
                case "ifmodifiedsince":
                case "if-modified-since":
                    request.IfModifiedSince = Convert.ToDateTime(value);
                    break;
                case "keepalive":
                case "keep-alive":
                    request.KeepAlive = Convert.ToBoolean(value);
                    break;
                case "proxy-connection":
                    break;
                case "referer":
                    request.Referer = value;
                    break;
                case "transferEncoding":
                    request.TransferEncoding = value;
                    break;
                case "useragent":
                case "user-agent":
                    request.UserAgent = value;
                    break;
                default:
                    request.Headers[key] = value;
                    break;
            }

            return request;
        }

        private static void AddRequestData(this WebRequest request, string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            request.ContentLength = data.Length;

            using (var streamWriter = request.GetRequestStream())
            {
                streamWriter.Write(data, 0, data.Length);
            }
        }
    }
}
