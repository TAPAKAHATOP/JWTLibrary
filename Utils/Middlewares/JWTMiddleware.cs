using System.Threading.Tasks;
using JWTLibrary.Interface;
using JWTLibrary.JWT;
using Microsoft.AspNetCore.Http;

namespace JWTLibrary.Utils.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJWTTokenProviderService TService;

        private readonly IJWTOptions JwtOptions;

        public JWTMiddleware(RequestDelegate next, IJWTTokenProviderService tService, IJWTOptions jwtOptions)
        {
            _next = next;
            this.TService = tService;
            this.JwtOptions = jwtOptions;
        }
        public async Task InvokeAsync(HttpContext context)
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
                    TokenData nToken = this.TService.RefreshToken(rToken);
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