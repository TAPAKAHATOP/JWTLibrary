using System.Threading.Tasks;
using JWTLibrary.Client;
using JWTLibrary.Interface;
using JWTLibrary.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JWTLibrary.Utils.Middlewares
{
    public class JWTMiddleware
    {
        private static string SIGNATURE = "sign";

        public ILogger<JWTMiddleware> Logger { get; }

        private readonly RequestDelegate _next;
        private readonly IJWTResolverService TService;

        private readonly IJWTOptions JwtOptions;

        public JWTMiddleware(ILogger<JWTMiddleware> logger, RequestDelegate next, IJWTResolverService tService, IJWTOptions jwtOptions)
        {
            this.Logger = logger;
            _next = next;
            this.TService = tService;
            this.JwtOptions = jwtOptions;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var aToken = context.Request.Cookies[TokenData.Access];
            var rToken = context.Request.Cookies[TokenData.Refresh];
            string signature = "";
            context.Request.Cookies.TryGetValue(SIGNATURE, out signature);

            if (!string.IsNullOrEmpty(aToken))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + aToken);
            }
            else
            {
                if (!string.IsNullOrEmpty(rToken))
                {
                    this.Logger.LogInformation("Start refresh access token by refresh");
                    TokenData nToken = this.TService.RefreshToken(rToken, signature).Result;
                    if (nToken != null)
                    {
                        context.Response.Cookies.Append(TokenData.Access, nToken.AccessToken, new CookieOptions() { MaxAge = JwtOptions.GetExpirationTimeSpanForAccessToken() });
                        context.Response.Cookies.Append(TokenData.Refresh, nToken.RefreshToken, new CookieOptions() { MaxAge = JwtOptions.GetExpirationTimeSpanForRefreshToken() });
                        context.Request.Headers.Add("Authorization", "Bearer " + nToken.AccessToken);
                    }
                }
            }

            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Xss-Protection", "1");
            context.Response.Headers.Add("X-Frame-Options", "DENY");

            await _next(context);
        }
    }
}