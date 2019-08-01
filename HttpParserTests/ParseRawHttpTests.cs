using HttpParser;
using HttpParser.Models;
using NUnit.Framework;

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

            Assert.AreEqual("https://httpbin.org/get", parsed.Url);
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

            Assert.AreEqual("https://httpbin.org/get", parsed.Url);
            Assert.AreEqual("GET", parsed.Headers["Method"]);
            Assert.AreEqual(null, parsed.RequestBody);
        }

        [Test]
        public void Should_Parse_Post()
        {
            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithQueryString);

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

            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithQueryString, options);

            Assert.AreEqual("https://httpbin.org/post", parsed.Url);
            Assert.AreEqual("POST", parsed.Headers["Method"]);
            Assert.AreEqual(null, parsed.RequestBody);
        }

        [TestCase(FakeRawRequests.BadlyFormattedRequest1, "")]
        [TestCase(FakeRawRequests.BadlyFormattedRequest2, "")]
        public void Should_Throw_For_Badly_Formatted_Request(string raw, string expectedMessage)
        {
            var ex = Assert.Throws<CouldNotParseHttpRequestException>(() => Parser.ParseRawRequest(raw));

            Assert.AreEqual(expectedMessage, ex.Message);
        }
    }
}
