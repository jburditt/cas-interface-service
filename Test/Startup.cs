public class Startup
{
    /// <summary>
    /// Register dependencies needed for xunit tests
    /// NOTE to register dependencies used by making calls from HttpClient, use CustomWebApplicationFactory
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        var fakeEnvironment = new FakeEnvironment
        {
            EnvironmentName = "Development",
        };

        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<AppSettings>()
            .AddEnvironmentVariables()
            .Build();

        services.AddAppSettings(fakeEnvironment);
        services.AddCasHttpClient(fakeEnvironment.IsProduction());
        services.AddServices();
        services.AddAutoMapperMappings();
        services.AddTransient<IPolicyProvider, PollyPolicyProvider>();
        // TODO remove after SSL cert is enabled on OpenShift
        services.AddHttpClient();
        services.AddTransient<ITokenProvider, TokenProvider>();
        services.AddDatabase(configuration);
    }

    public class FakeEnvironment : IWebHostEnvironment
    {
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string ApplicationName { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public string EnvironmentName { get; set; }
    }
}