﻿public interface ITokenProvider
{
    Task<string> GetAccessTokenAsync();
    Task RefreshTokenAsync();
}