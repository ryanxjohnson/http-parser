using FluentAssertions;
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
            parsed.Url.Should().BeEquivalentTo("https://httpbin.org/get");
            parsed.Headers["Method"].Should().BeEquivalentTo("GET");
            parsed.RequestBody.Should().BeNull();
        }

        [Test]
        public void Should_Parse_Get_With_QueryString()
        {
            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithQueryString);

            parsed.Url.Should().BeEquivalentTo("https://httpbin.org/get?name=ryan");
            parsed.Headers["Method"].Should().BeEquivalentTo("GET");
            parsed.RequestBody.Should().BeEquivalentTo("name=ryan");
        }

        [Test]
        public void Should_Parse_Get_With_QueryString_Ignored()
        {
            var options = new IgnoreHttpParserOptions
            {
                IgnoreRequestBody = true
            };

            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithQueryString, options);

            parsed.Url.Should().BeEquivalentTo("https://httpbin.org/get?name=ryan");
            parsed.Headers["Method"].Should().BeEquivalentTo("GET");
            parsed.RequestBody.Should().BeNull();
        }

        [Test]
        public void Should_Parse_Post()
        {
            var parsed = Parser.ParseRawRequest(FakeRawRequests.PostWithRequestBody);

            parsed.Url.Should().BeEquivalentTo("https://httpbin.org/post");
            parsed.Headers["Method"].Should().BeEquivalentTo("POST");
            parsed.RequestBody.Should().BeEquivalentTo("helloworld");
        }

        [Test]
        public void Should_Parse_Post_Ignore_RequestBody()
        {
            var options = new IgnoreHttpParserOptions
            {
                IgnoreRequestBody = true
            };

            var parsed = Parser.ParseRawRequest(FakeRawRequests.PostWithRequestBody, options);

            parsed.Url.Should().BeEquivalentTo("https://httpbin.org/post");
            parsed.Headers["Method"].Should().BeEquivalentTo("POST");
            parsed.Cookies.Should().HaveCount(1);
            parsed.Cookies["ilikecookies"].Should().BeEquivalentTo("chocchip");
            parsed.RequestBody.Should().BeNull();
        }

        [TestCase(FakeRawRequests.BadlyFormattedRequest1, "URL is not in a valid format Method: SetUrl() Data: www.httpbin.org/get")]
        public void Should_Throw_For_Badly_Formatted_Request(string raw, string expectedMessage)
        {
            var ex = Assert.Throws<CouldNotParseHttpRequestException>(() => Parser.ParseRawRequest(raw));

            ex.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void Should_Not_Throw_For_Extra_Lines()
        {
            Assert.DoesNotThrow(() => Parser.ParseRawRequest(FakeRawRequests.BadlyFormattedRequest2));
        }

        [Test]
        public void Should_Parse_Cookie_In_Wrong_Place()
        {
            var raw = FakeRawRequests.RequestWithCookiesInTheWrongSpot;
            var parsed = Parser.ParseRawRequest(raw);

            System.Console.WriteLine(parsed.Cookies.Count);
        }
    }
}
