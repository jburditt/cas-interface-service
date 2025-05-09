namespace Client;

public class CasHttpClient : ICasHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly Model.Settings.Client _settings;
    private readonly ILogger<CasHttpClient> _logger;

    public CasHttpClient(/* TODO HttpClient httpClient, */Model.Settings.Client settings, ILogger<CasHttpClient> logger)
    {
        _settings = settings;
        _logger = logger;

        // TODO ignore ssl cert errors, remove this code after SSL cert is working on OpenShift
        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        var httpClient = new HttpClient(httpClientHandler);

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.BaseAddress = new Uri(settings.BaseUrl);
        httpClient.Timeout = new TimeSpan(1, 0, 0);  // 1 hour timeout 
        _httpClient = httpClient;
    }

    public async Task<Response> Get(string url)
    {
        // TODO hack until SSL cert is installed
        var tokenProvider = new TokenProvider(_httpClient, _settings, null);
        await tokenProvider.RefreshTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenProvider.GetAccessTokenAsync());

        var response = await _httpClient.GetAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("RESPONSE {0}", responseContent);
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
        // TODO hack until SSL cert is installed
        var tokenProvider = new TokenProvider(_httpClient, _settings, null);
        await tokenProvider.RefreshTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenProvider.GetAccessTokenAsync());

        var postContent = new StringContent(payload);
        var response = await _httpClient.PostAsync(url, postContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("RESPONSE {0}", responseContent);
        return new(responseContent, response.StatusCode);
    }
}

public static class CasHttpClientExtensions
{
    public static IServiceCollection AddCasHttpClient(this IServiceCollection services, bool isProduction)
    {
        services
            // TODO uncomment after SSL cert is working in OpenShift
            //.AddTransient<IgnoreSslClientHandler>()
            //.AddTransient<ITokenProvider, TokenProvider>()
            //.AddTransient<TokenDelegatingHandler>()
            .AddTransient<ICasService, CasService>()
            .AddTransient<ICasHttpClient, CasHttpClient>()
            //.AddHttpClient<ICasHttpClient, CasHttpClient>()
            //    .ConfigurePrimaryHttpMessageHandler<IgnoreSslClientHandler>()
            //    .AddHttpMessageHandler<TokenDelegatingHandler>()
            ;

        return services;
    }
}
