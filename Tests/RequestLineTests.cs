using HttpParser.Models;
using NUnit.Framework;
using FluentAssertions;
namespace HttpParserTests
{
    [TestFixture]
    public class RequestLineTests
    {
        [Test]
        public void Should_Parse_Request_Line()
        {
            var line = new [] { "GET https://www.example.com HTTP/1.1" };

            var requestLine = new RequestLine(line);

            requestLine.Method.Should().BeEquivalentTo("GET");
            requestLine.Url.Should().BeEquivalentTo("https://www.example.com");
            requestLine.HttpVersion.Should().BeEquivalentTo("HTTP/1.1");
        }

        [Test]
        public void Should_Throw_For_Bad_Method()
        {
            var line = new[] { "PUT https://www.example.com HTTP/1.1" };

            var ex = Assert.Throws<CouldNotParseHttpRequestException>(() => new RequestLine(line));
            ex.Message.Should().BeEquivalentTo("Not a valid HTTP Verb Method: SetHttpMethod() Data: PUT");
        }

        [Test]
        public void Should_Throw_For_Bad_Url()
        {
            var line = new[] { "GET www.example.com HTTP/1.1" };

            var ex = Assert.Throws<CouldNotParseHttpRequestException>(() => new RequestLine(line));
            ex.Message.Should().BeEquivalentTo("URL is not in a valid format Method: SetUrl() Data: www.example.com");
        }
    }
}
