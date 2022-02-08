using System;
using System.Threading.Tasks;
using JWTLibrary.Client;
using JWTLibrary.Interface;
using JWTLibrary.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JWTLibrary.Utils.Middlewares
{
    public class JWTDefaultMiddleware : ABaseJWTMiddleware<JWTDefaultMiddleware>
    {
        public JWTDefaultMiddleware(ILogger<JWTDefaultMiddleware> logger, RequestDelegate next, IJWTResolverService tService, IJWTLifeTimeOptions jwtOptions)
        : base(logger, next, tService, jwtOptions)
        { }

        public override async Task InvokeAsync(HttpContext context)
        {
            var aToken = context.Request.Cookies[TokenData.Access];
            var rToken = context.Request.Cookies[TokenData.Refresh];

            if (!string.IsNullOrEmpty(aToken))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + aToken);
            }
            else
            {
                if (!string.IsNullOrEmpty(rToken))
                {
                    this.Logger.LogInformation("Start refresh access token by refresh");
                    TokenData nToken = this.TService.RefreshToken(rToken).Result;
                    if (nToken != null)
                    {
                        context.Response.Cookies.Append(TokenData.Access, nToken.AccessToken, new CookieOptions() { MaxAge = JwtOptions.ExpirationTimeSpanForAccessToken });
                        context.Response.Cookies.Append(TokenData.Refresh, nToken.RefreshToken, new CookieOptions() { MaxAge = JwtOptions.ExpirationTimeSpanForRefreshToken });
                        context.Request.Headers.Add("Authorization", "Bearer " + nToken.AccessToken);
                    }
                    else
                    {
                        context.Response.Cookies.Delete(TokenData.Refresh);
                    }
                }
            }
            await _next(context);
        }
    }
}