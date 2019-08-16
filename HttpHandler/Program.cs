using System;
using System.Net;

namespace HttpHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var rr = new RequestRunner();

            try
            {
                rr.Run();
            }
            catch(WebException wex)
            {
                Console.WriteLine($"Web exception caught. {wex.Message}");
            }
        }
    }
}
