namespace Tests.FakeData
{
    public class FakeRawRequests
    {
        public const string GetWithoutQueryString = @"GET https://httpbin.org/get HTTP/1.1
Host: httpbin.org
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36
Upgrade-Insecure-Requests: 1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9";


        public const string GetWithQueryString = @"GET https://httpbin.org/get?name=ryan HTTP/1.1
Host: httpbin.org
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36
Upgrade-Insecure-Requests: 1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9";

        public const string PostWithRequestBody = @"POST https://httpbin.org/post HTTP/1.1
Host: httpbin.org
User-Agent: curl/7.54.1
Accept: */*
Content-Type: application/x-www-form-urlencoded
Cookie: ilikecookies=chocchip

helloworld";

        public const string BadlyFormattedRequest1 = @"GET www.httpbin.org/get HTTP/1.1
Host: httpbin.org
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36
Upgrade-Insecure-Requests: 1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9";

        public const string BadlyFormattedRequest2 = @"POST https://httpbin.org/post HTTP/1.1
Host: httpbin.org
User-Agent: curl/7.54.1
Accept: */*
Content-Type: application/x-www-form-urlencoded
Cookie: ilikecookies=chocchip

helloworld

";

        public const string RequestWithCookiesInTheWrongSpot = @"POST http://www.providerlookuponline.com/Coventry/po7/Client_FacetWebService.asmx/FillStateFacet HTTP/1.1
Accept: application/json, text/javascript, */*; q=0.01
Origin: http://www.providerlookuponline.com
X-Requested-With: XMLHttpRequest
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36
Content-Type: application/json; charset=UTF-8
Referer: http://www.providerlookuponline.com/coventry/po7/Search.aspx
Accept-Encoding: gzip, deflate
Host: www.providerlookuponline.com
Cookie: ASP.NET_SessionId=yayd4qyttoe1xgq0xgc24qmy; TS019e6c20=014b5a756ffb575a91f7fe2504052493bb4f1fba160f72df0ba31f8e13fd05c3d62743577fe69ccf443cff87a9d33c5a5f3cc5a96c9e6ba3933a2150b7925ed7a82f86fba4; TS019e6c20_28=014d91d154ab4251246f2b614bad8e3e3241d256a621c0c978e5b5b0c957f5a69eb8d190406800a83fe9c55d4a39c60544f14384e4; TS96fa379b_75=TS96fa379b_rc=0&TS96fa379b_id=2&TS96fa379b_cr=08826a2764ab2800a727f3fb31df5fa1f3d7126b7eb93d1972e2ff558b0f89972be6c6a81e4f0ecc37725679d56547d4:0801e2461a032000f07dc4289936b17d79f52446a01f35ddc8aedb7f8aec47131ce115d972fb6fb8&TS96fa379b_ef=&TS96fa379b_pg=0&TS96fa379b_ct=0&TS96fa379b_rf=0; TSPD_101=08826a2764ab2800a727f3fb31df5fa1f3d7126b7eb93d1972e2ff558b0f89972be6c6a81e4f0ecc37725679d56547d4:; GeoCookie=VisitorGUID=c121d0f4-b987-443d-905a-0d015b138eaa; InitialIntegrationComplete=True; __utma=1.1241581742.1539971847.1539971847.1539971847.1; __utmc=1; __utmz=1.1539971847.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); __utmt=1; __utmb=1.1.10.1539971847; TS019e6c20_77=08826a2764ab2800c69276fe08e7da9f855eefc774f68568df90d2fb919995f7424c2808379f3342dcf98983f849918c08cbf3bf3d823800b90ffcee0cb1a32f5eb378991711baafb033fee7eedb1b0ceaec62f398876aef21bfefc413a3f8f81dc6e49b9a3ae3c2d6f47a28e1ba2d72; GeoCookie=VisitorGUID=c121d0f4-b987-443d-905a-0d015b138eaa; TS01b76a60=014b5a756f4c4111c831124ec77a44bbd45d6d53ed0f72df0ba31f8e13fd05c3d62743577f32f32b5e15dd86c365179a3c6bdbfcf5b69a009d8cebfcc98e7f845df02080ee; ASP.NET_SessionId=yayd4qyttoe1xgq0xgc24qmy; TS019e6c20=014b5a756ffb575a91f7fe2504052493bb4f1fba160f72df0ba31f8e13fd05c3d62743577fe69ccf443cff87a9d33c5a5f3cc5a96c9e6ba3933a2150b7925ed7a82f86fba4; TS01b76a60=014b5a756f4c4111c831124ec77a44bbd45d6d53ed0f72df0ba31f8e13fd05c3d62743577f32f32b5e15dd86c365179a3c6bdbfcf5b69a009d8cebfcc98e7f845df02080ee; TS019e6c20_28=014d91d154ab4251246f2b614bad8e3e3241d256a621c0c978e5b5b0c957f5a69eb8d190406800a83fe9c55d4a39c60544f14384e4
Content-Length: 23

#{{RequestBody}}";
    }
}
