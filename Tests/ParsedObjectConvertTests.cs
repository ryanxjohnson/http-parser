using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ParsedObjectConvertTests
    {
        [TestCase(FakeData.FakeRawRequests.GetWithoutQueryString)]
        [TestCase(FakeData.FakeRawRequests.PostWithRequestBody)]
        public void Should_Convert_ParsedRequest_Back_To_String(string input)
        {
            var parsed = HttpParser.Parser.ParseRawRequest(input);

            Assert.AreEqual(input, parsed.ToString());
        }

        [TestCase(FakeData.FakeRawRequests.PostWithRequestBody)]
        public void Should_Strip_Cookies(string input)
        {
            var parsed = HttpParser.Parser.ParseRawRequest(input, new HttpParser.Models.IgnoreHttpParserOptions { IgnoreCookies = true }); ;

            Assert.AreEqual(requestCookiesStripped, parsed.ToString());
        }

        private string requestCookiesStripped = @"POST https://httpbin.org/post HTTP/1.1
Host: httpbin.org
User-Agent: curl/7.54.1
Accept: */*
Content-Type: application/x-www-form-urlencoded

helloworld";
    }
}
