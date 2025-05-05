public interface ITokenProvider
{
    Task<string> GetAccessTokenAsync();
    Task<string> RefreshTokenAsync();
}