using System;
using System.Linq;
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

            if (string.IsNullOrEmpty(aToken))
            {
                if (!string.IsNullOrEmpty(rToken))
                {
                    this.Logger.LogInformation("Start refresh access token by refresh");
                    TokenData nToken = this.TService.RefreshToken(rToken).Result;
                    if (nToken != null)
                    {
                        aToken = nToken.AccessToken;
                        rToken = nToken.RefreshToken;
                        context.Response.Cookies.Append(TokenData.Access, nToken.AccessToken, new CookieOptions() { MaxAge = JwtOptions.ExpirationTimeSpanForAccessToken });
                        context.Response.Cookies.Append(TokenData.Refresh, nToken.RefreshToken, new CookieOptions() { MaxAge = JwtOptions.ExpirationTimeSpanForRefreshToken });
                    }
                    else
                    {
                        this.Logger.LogInformation("Refreshing user token failed;");
                        context.Response.Cookies.Delete(TokenData.Refresh);
                    }
                }
            }

            if (!string.IsNullOrEmpty(aToken))
            {
                if ((new string[] { "post", "delete", "put", "patch" }).Contains(context.Request.Method.ToLower()))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + aToken);
                }
                else
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + aToken);
                }
            }

            await _next(context);
        }
    }
}