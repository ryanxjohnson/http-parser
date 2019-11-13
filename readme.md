# Http Web Parser

## What this libary will accomplish:
This library will parse raw HTTP request text to a C# object.
This allows you save all the information about a particular web request in permanent storage, decoupling the request properties from any particular framework.

This libary will also build a .NET `HttpWebRequest` object from the raw request text or the JSON object.

IgnoreSerialization options allows the client to ignore certain headers like cookies, headers, etc.

## Parsing Usage:

Sample Raw Web Request
```
var raw = "
GET https://httpbin.org/get HTTP/1.1
Host: httpbin.org
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36
Upgrade-Insecure-Requests: 1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9";
```

#### IgnoreHttpParserOptions:
You can specify what the parser should not serialize. Pass IgnoreHttpParserOptions as an optional parameter to the parser. For example, it might not make sense to parse the request body if you are replacing with it with another value.

```
IgnoreHttpParserOptions options = new IgnoreHttpParserOptions { IgnoreRequestBody = true };
```

### Parse to ParsedRequest object:
```
var parsed = Parser.ParseRawRequest(raw);
```

## Build .NET HttpWebRequest Usage:

```
HttpWebRequest request = HttpWebRequestBuilder.InitializeWebRequest(parsed);
```

## Invoking callback in InitializeWebRequest()
Calling `request.GetRequestStream()` closes the request for adding headers, so unless you are positive you don't need to add any new headers after writing the request body, use the call back to defer this to your client

### What this looks like:
```
// build request
HttpWebRequest request = ... build request

// defer control back to the calling method
callback?.Invoke(request);

// add request body
request.WritePostDataToRequestStream(requestBody);
```

### Sample RequestBuilder invoking callback
```
void ClientBuildRequest()
{
// parse raw request...

HttpWebRequest request = HttpWebRequestBuilder.InitializeWebRequest(parsed, AddMoreDynamicHeaders);

// do stuff with the completed request
}

static void AddMoreDynamicHeaders(HttpWebRequest request)
{
// request.Headers.Add(...)
}
```

### Execute Web Request and capture response:
```
using (var httpWebResponse = request.GetResponse())
{
	httpWebResponse.GetParsedWebResponse();
}
```

### ParsedResponse is a flattened version of HttpWebResponse
```
string ResponseText 
int StatusCode
string StatusDescription
string Cookies
Uri ResponseUri
Dictionary<string, string[]> ResponseHeaders
```


### Mocking Web Requests in unit tests with Moq

```
var response = new Mock<IHttpWebResponse>();
response.Setup(s => s.GetParsedWebResponse()).Returns(new ParsedWebResponse { ResponseText = "Hello world" });

var request = new Mock<IHttpWebRequest>();
request.Setup(c => c.GetResponse()).Returns(response.Object);

var factory = new Mock<IHttpWebRequestFactory>();
            factory.Setup(c => c.BuildRequest(It.IsAny<ParsedHttpRequest>())).Returns(request.Object);
			
var parsed = Parser.ParseRawRequest("GET http://www.foo.com HTTP/1.1");

var result = factory.Object.BuildRequest(parsed).GetResponse();

Console.WriteLine(result); // "Hello world"
```
