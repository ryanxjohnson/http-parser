using HttpWebRequestExecutor.Models;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.Net;

namespace HttpWebRequestExecutor.Extensions
{
    public static class HttpWebResponseExtensions
    {
        public static ParsedWebResponse ExecuteRequestGetParsedWebResponse(this HttpWebRequest request)
        {
            using (var resp = (HttpWebResponse)request.GetResponse())
            {
                return new ParsedWebResponse(resp);
            }
        }

        public static string ResponseString(this HttpWebResponse resp)
        {
            if (resp.Headers["Content-Encoding"] == "gzip")
            {
                using (var stream = resp.GetResponseStream())
                {
                    if (stream != null)
                        using (var streamReader = new StreamReader(resp.GetGzipStream()))
                        {
                            return streamReader.ReadToEnd();
                        }
                }
            }

            using (var stream = resp.GetResponseStream())
                if (stream != null)
                    using (var streamReader = new StreamReader(stream))
                        return streamReader.ReadToEnd();

            return null;
        }

        public static Stream GetGzipStream(this HttpWebResponse response)
        {
            Stream compressedStream = null;
            if (response.ContentEncoding == "gzip")
                compressedStream = new GZipInputStream(response.GetResponseStream());

            if (compressedStream == null)
                return response.GetResponseStream();

            var decompressedStream = new MemoryStream();
            var size = 2048;
            var writeData = new byte[size];

            while (true)
            {
                size = compressedStream.Read(writeData, 0, size);
                if (size > 0)
                {
                    decompressedStream.Write(writeData, 0, size);
                }
                else
                {
                    break;
                }
            }

            decompressedStream.Seek(0, SeekOrigin.Begin);

            return decompressedStream;
        }
    }
}
