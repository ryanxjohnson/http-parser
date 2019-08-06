﻿using HttpBuilder;
using HttpBuilder.Interfaces;
using HttpParser;
using HttpParserTests;
using NUnit.Framework;
using System.IO;
using System.Text;
using Moq;
using HttpParser.Models;
using System.Net;

namespace Tests
{
    [TestFixture]
    public class HttpWebRequestBuilderTests
    {
        [Test]
        public void Should_Build_Get()
        {
            var parsed = Parser.ParseRawRequest(FakeRawRequests.GetWithoutQueryString);
            var req = HttpWebRequestBuilder.InitializeRequest(parsed);

            Assert.AreEqual("System.Net.HttpWebRequest", req.GetType().ToString());
        }

        [Test]
        public void Should_Run_Fake_Request()
        {
            // arrange
            var expected = "hello world";

            var response = new Moq.Mock<IHttpWebResponse>();
            response.Setup(s => s.GetResponseStream()).Returns(FakeStream(expected));

            var request = new Moq.Mock<IHttpWebRequest>();
            request.Setup(c => c.GetResponse()).Returns(response.Object);

            var factory = new Moq.Mock<IHttpWebRequestFactory>();
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
            Assert.AreEqual(expected, actual);
        }

        private static MemoryStream FakeStream(string expected)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(expected);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            return responseStream;
        }
    }
}