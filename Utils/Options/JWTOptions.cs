using System;
using JWTLibrary.Interface;

namespace JWTLibrary.Utils.Options
{
    public class JWTOptions : IJWTOptions
    {
        public int AccessTokenLifeTime { get; set; }
        public int RefreshTokenLifeTime { get; set; }

        public TimeSpan GetExpirationTimeSpanForAccessToken()
        {
            return TimeSpan.FromMinutes(AccessTokenLifeTime);
        }

        public TimeSpan GetExpirationTimeSpanForRefreshToken()
        {
            return TimeSpan.FromMinutes(RefreshTokenLifeTime);
        }
    }
}