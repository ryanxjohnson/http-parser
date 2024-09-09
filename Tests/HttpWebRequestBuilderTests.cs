using HttpBuilder;
using HttpParser;
using NUnit.Framework;
using System.IO;
using System.Text;
using Moq;
using HttpParser.Models;
using HttpWebRequestExecutor.Models;
using Tests.FakeData;
using HttpWebRequestExecutor.Interfaces;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class HttpWebRequestBuilderTests
    {
        [Test]
        public void Should_Build_Get()
        {
            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithoutQueryString);
            var req = HttpWebRequestBuilder.InitializeWebRequest(parsed);

            req.Should().BeOfType<System.Net.HttpWebRequest>();
        }

        [Test]
        public void Should_Run_Fake_Request()
        {
            // arrange
            var expected = "hello world";

            var response = new Mock<IHttpWebResponse>();
            response.Setup(s => s.GetResponseStream()).Returns(FakeStream(expected));

            var request = new Mock<IHttpWebRequest>();
            request.Setup(c => c.GetResponse()).Returns(response.Object);

            var factory = new Mock<IHttpWebRequestFactory>();
            factory.Setup(c => c.BuildRequest(It.IsAny<ParsedHttpRequest>())).Returns(request.Object);

            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithoutQueryString);

            // act
            var actualRequest = factory.Object.BuildRequest(parsed);

            string actual;

            using (var httpWebResponse = actualRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
            }

            // assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Should_Get_Fake_ParsedWebResponse()
        {
            // arrange
            var expected = "hello world";

            var response = new Mock<IHttpWebResponse>();
            response.Setup(s => s.GetParsedWebResponse()).Returns(FakeParsedWebResponse(expected));

            var request = new Mock<IHttpWebRequest>();
            request.Setup(c => c.GetResponse()).Returns(response.Object);

            var factory = new Mock<IHttpWebRequestFactory>();
            factory.Setup(c => c.BuildRequest(It.IsAny<ParsedHttpRequest>())).Returns(request.Object);

            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithoutQueryString);

            // act
            var actualRequest = factory.Object.BuildRequest(parsed);

            string actual;

            using (var httpWebResponse = actualRequest.GetResponse())
            {
                actual = httpWebResponse.GetParsedWebResponse().ResponseText;
            }

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        private static MemoryStream FakeStream(string expected)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(expected);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            return responseStream;
        }

        private static ParsedWebResponse FakeParsedWebResponse(string responseText)
        {
            return new ParsedWebResponse()
            {
                ResponseText = responseText
            };
        }
    }
}
