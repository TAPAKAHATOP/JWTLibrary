using System;
using JWTLibrary.Interface;

namespace JWTLibrary.Utils.Options
{
    public class JWTOptions : IJWTLifeTimeOptions
    {
        public TimeSpan ExpirationTimeSpanForAccessToken { get; set; }
        public TimeSpan ExpirationTimeSpanForRefreshToken { get; set; }
    }
}