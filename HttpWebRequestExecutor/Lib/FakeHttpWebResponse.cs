using HttpBuilder.Interfaces;
using System;
using System.IO;
using System.Net;

namespace HttpBuilder
{
    public class FakeHttpWebResponse : IHttpWebResponse
    {
        private HttpWebResponse response;

        public FakeHttpWebResponse(HttpWebResponse response)
        {
            this.response = response;
        }

        public Stream GetResponseStream()
        {
            return response.GetResponseStream();
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
