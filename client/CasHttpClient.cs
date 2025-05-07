namespace Client;

public class CasHttpClient : ICasHttpClient
{
    private readonly HttpClient _httpClient = null;

    public CasHttpClient(HttpClient httpClient, Model.Settings.Client settings)
    {
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.BaseAddress = new Uri(settings.BaseUrl);
        httpClient.Timeout = new TimeSpan(1, 0, 0);  // 1 hour timeout 
        _httpClient = httpClient;
    }

    public async Task<Response> Get(string url)
    {
        var response = await _httpClient.GetAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseStatusCode = response.StatusCode;
        // this is a hack work-around for CAS returning 404 instead of 204. When we get a 404 and json response with "code" = "NotFound", we know CAS really means a 204, the object didn't exist e.g. the invoice number didn't exist in the database
        if (response.StatusCode == HttpStatusCode.NotFound && !string.IsNullOrEmpty(responseContent))
        {
            try
            {
                var json = JObject.Parse(responseContent);
                if (json.ContainsKey("code") && json.GetValue("code").Value<string>() == "NotFound")
                {
                    responseContent = null;
                    responseStatusCode = HttpStatusCode.NoContent;
                }
            } catch (Exception ex) {  }
        }
        return new Response(responseContent, responseStatusCode);
    }

    public async Task<Response> Post(string url, string payload)
    {
        var postContent = new StringContent(payload);
        var response = await _httpClient.PostAsync(url, postContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        return new(responseContent, response.StatusCode);
    }
}

public static class CasHttpClientExtensions
{
    public static IServiceCollection AddCasHttpClient(this IServiceCollection services, bool isProduction)
    {
        services
            .AddTransient<IgnoreSslClientHandler>()
            .AddTransient<ITokenProvider, TokenProvider>()
            .AddTransient<TokenDelegatingHandler>()
            .AddTransient<ICasService, CasService>()
            .AddHttpClient<ICasHttpClient, CasHttpClient>()
                .ConfigurePrimaryHttpMessageHandler<IgnoreSslClientHandler>()
                .AddHttpMessageHandler<TokenDelegatingHandler>();

        return services;
    }
}
