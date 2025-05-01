public class CasHttpClientTests(ICasHttpClient casHttpClient, AppSettings appSettings)
{
    [Fact]
    public async Task Get_Token_Success()
    {
        casHttpClient.Initialize(appSettings.Client, appSettings.IsProduction);

        var statusCode = await casHttpClient.GetToken();

        Assert.Equal(HttpStatusCode.OK, statusCode);
    }
}
