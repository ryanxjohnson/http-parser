using HttpWebRequestExecutor.Interfaces;
using HttpWebRequestExecutor.Models;
using System;
using System.IO;
using System.Net;

namespace HttpWebRequestExecutor.Lib
{
    internal class HttpWebResponseWrapper : IHttpWebResponse
    {
        private HttpWebResponse response;

        public HttpWebResponseWrapper(HttpWebResponse response)
        {
            this.response = response;
        }

        public Stream GetResponseStream()
        {
            return response.GetResponseStream();
        }

        public ParsedWebResponse GetParsedWebResponse()
        {
            return new ParsedWebResponse(response);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || response == null) return;

            response.Dispose();
            response = null;
        }
    }
}
