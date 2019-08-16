using HttpWebRequestExecutor.Models;
using System;
using System.IO;

namespace HttpBuilder.Interfaces
{
    public interface IHttpWebResponse : IDisposable
    {
        Stream GetResponseStream();
        ParsedWebResponse GetParsedWebResponse();
    }
}
