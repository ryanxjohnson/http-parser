using HttpWebRequestExecutor.Factories;
using HttpWebRequestExecutor.Interfaces;
using System;
using System.Net;

namespace HttpHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IHttpWebRequestFactory factory = new HttpWebRequestFactory();
                var rr = new RequestRunner(factory);
                rr.Run();
            }
            catch(WebException wex)
            {
                Console.WriteLine($"Web exception caught. {wex.Message}");
            }
        }
    }
}
