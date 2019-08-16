using ICSharpCode.SharpZipLib.GZip;
using System.IO;

namespace HttpWebRequestExecutor.Extensions
{
    public static class StreamExtensions
    {
        public static string GetStringFromStream(this Stream stream)
        {
            if (stream == null) return null;

            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static Stream DecompressGzipStream(this Stream stream)
        {
            if (stream == null) return stream;

            Stream compressedStream = new GZipInputStream(stream);

            if (compressedStream == null)
            {
                return stream;
            }

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
