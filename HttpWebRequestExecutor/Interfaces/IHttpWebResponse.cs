using HttpWebRequestExecutor.Models;
using System;
using System.IO;

namespace HttpWebRequestExecutor.Interfaces
{
    public interface IHttpWebResponse : IDisposable
    {
        Stream GetResponseStream();
        ParsedWebResponse GetParsedWebResponse();
    }
}
