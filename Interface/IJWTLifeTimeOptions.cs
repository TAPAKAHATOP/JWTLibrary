using System;

namespace JWTLibrary.Interface
{
    public interface IJWTLifeTimeOptions
    {
        TimeSpan ExpirationTimeSpanForAccessToken { get; set; }
        TimeSpan ExpirationTimeSpanForRefreshToken { get; set; }

    }
}