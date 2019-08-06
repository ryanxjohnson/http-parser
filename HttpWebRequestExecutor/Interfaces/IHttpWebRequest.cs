namespace HttpBuilder.Interfaces
{
    public interface IHttpWebRequest
    {
        string Method { get; set; }
        IHttpWebResponse GetResponse();
    }
}
