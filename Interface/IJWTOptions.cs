using System;

namespace JWTLibrary.Interface
{
    public interface IJWTOptions
    {
        int RefreshTokenLifeTime { get; set; }
        int AccessTokenLifeTime { get; set; }
        TimeSpan GetExpirationTimeSpanForAccessToken();
        TimeSpan GetExpirationTimeSpanForRefreshToken();

    }
}