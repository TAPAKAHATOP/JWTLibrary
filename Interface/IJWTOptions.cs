using System;

namespace JWTLibrary.Interface
{
    public interface IJWTOptions : IJWTLifeTimeOptions
    {
        TimeSpan GetExpirationTimeSpanForAccessToken();
        TimeSpan GetExpirationTimeSpanForRefreshToken();

    }
}