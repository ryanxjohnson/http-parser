using HttpWebRequestExecutor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HttpWebRequestExecutor.Models
{
    public class ParsedWebResponse
    {
        public string ResponseText { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Cookies { get; set; }
        public Uri ResponseUri { get; set; }
        public Dictionary<string, string[]> ResponseHeaders { get; set; }

        public ParsedWebResponse(HttpWebResponse response)
        {
            ResponseText = response.ResponseString();
            StatusCode = (int)response.StatusCode;
            StatusDescription = response.StatusDescription;
            ResponseHeaders = ConvertWebHeadersToDictionary(response.Headers);
            Cookies = response.Headers["Set-Cookie"];
            ResponseUri = response.ResponseUri;
        }

        private Dictionary<string, string[]> ConvertWebHeadersToDictionary(WebHeaderCollection headers)
        {
            return Enumerable.Range(0, headers.Count).ToDictionary(i => headers.Keys[i], headers.GetValues);
        }
    }
}
