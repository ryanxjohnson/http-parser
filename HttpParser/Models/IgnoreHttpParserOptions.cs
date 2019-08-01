namespace HttpParser.Models
{
    public class IgnoreHttpParserOptions
    {
        public bool IgnoreUrl { get; set; }
        public bool IgnoreHeaders { get; set; }        
        public bool IgnoreCookies { get; set; }
        public bool IgnoreRequestBody { get; set; }
    }
}
