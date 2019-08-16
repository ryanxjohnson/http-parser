using HttpParser;
using HttpParser.Models;
using NUnit.Framework;
using Tests.FakeData;

namespace HttpParserTests
{
    [TestFixture()]
    public class ParseRawHttpTests
    {
        [Test]
        public void Should_Parse_Get()
        {
            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithoutQueryString);

            Assert.AreEqual("https://httpbin.org/get", parsed.Url);
            Assert.AreEqual("GET", parsed.Headers["Method"]);
            Assert.AreEqual(null, parsed.RequestBody);
        }

        [Test]
        public void Should_Parse_Get_With_QueryString()
        {
            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithQueryString);

            Assert.AreEqual("https://httpbin.org/get?name=ryan", parsed.Url);
            Assert.AreEqual("GET", parsed.Headers["Method"]);
            Assert.AreEqual("name=ryan", parsed.RequestBody);
        }

        [Test]
        public void Should_Parse_Get_With_QueryString_Ignored()
        {
            var options = new IgnoreHttpParserOptions
            {
                IgnoreRequestBody = true
            };

            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithQueryString, options);

            Assert.AreEqual("https://httpbin.org/get?name=ryan", parsed.Url);
            Assert.AreEqual("GET", parsed.Headers["Method"]);
            Assert.AreEqual(null, parsed.RequestBody);
        }

        [Test]
        public void Should_Parse_Post()
        {
            var parsed = Parser.ParseRawRequest(FakeRawRequests.PostWithRequestBody);

            Assert.AreEqual("https://httpbin.org/post", parsed.Url);
            Assert.AreEqual("POST", parsed.Headers["Method"]);
            Assert.AreEqual("helloworld", parsed.RequestBody);
        }

        [Test]
        public void Should_Parse_Post_Ignore_RequestBody()
        {
            var options = new IgnoreHttpParserOptions
            {
                IgnoreRequestBody = true
            };

            var parsed = Parser.ParseRawRequest(FakeRawRequests.PostWithRequestBody, options);

            Assert.AreEqual("https://httpbin.org/post", parsed.Url);
            Assert.AreEqual("POST", parsed.Headers["Method"]);
            Assert.AreEqual(1, parsed.Cookies.Count);
            Assert.AreEqual("chocchip", parsed.Cookies["ilikecookies"]);
            Assert.AreEqual(null, parsed.RequestBody);
        }

        [TestCase(FakeRawRequests.BadlyFormattedRequest1, "URL is not in a valid format: www.httpbin.org/get")]
        public void Should_Throw_For_Badly_Formatted_Request(string raw, string expectedMessage)
        {
            var ex = Assert.Throws<CouldNotParseHttpRequestException>(() => Parser.ParseRawRequest(raw));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        public void Should_Not_Throw_For_Extra_Lines()
        {
            Assert.DoesNotThrow(() => Parser.ParseRawRequest(FakeRawRequests.BadlyFormattedRequest2));
        }
    }
}
