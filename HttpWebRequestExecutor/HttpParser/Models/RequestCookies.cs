using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HttpParser.Models
{
    internal class RequestCookies
    {
        public Dictionary<string, string> ParsedCookies = new Dictionary<string, string>();
        public RequestCookies(string[] lines)
        {
            var cookieLine = ExtractCookiesLine(lines);
            PopulateParsedCookies(cookieLine);
        }

        private string ExtractCookiesLine(string[] lines)
        {
            var cookieIndex = Array.FindLastIndex(lines, l => l.StartsWith("Cookie"));

            return cookieIndex > 0 ? lines[cookieIndex] : null;
        }

        private void PopulateParsedCookies(string cookiesLine)
        {
            if (string.IsNullOrEmpty(cookiesLine)) return;

            var matches = new Regex(@"Cookie:(?<Cookie>(.+))", RegexOptions.Singleline).Match(cookiesLine);
            var cookies = matches.Groups["Cookie"].ToString().Trim().Split(';');

            if (cookies?.Length < 1 || cookies.Contains(""))
            {
                return;
            }

            foreach (var cookie in cookies)
            {
                var key = cookie.Split('=')[0].Trim();
                var value = cookie.Split('=')[1].Trim();
                ParsedCookies[key] = value;
            }
        }
    }
}
