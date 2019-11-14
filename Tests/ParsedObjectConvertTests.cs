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
    }
}
