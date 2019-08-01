using System;

namespace HttpParser.Models
{
    public class CouldNotParseHttpRequestException : Exception
    {
        public CouldNotParseHttpRequestException(string message) : base(message) { }
    }
}
