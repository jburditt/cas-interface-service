namespace Client;

public interface ICasHttpClient
{
    Task<Response> Get(string url);
    Task<Response> Post(string url, string payload);
}