using JWTLibrary.Utils.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace JWTLibrary.Utils
{
    public static class JWTMiddlewareExtension
    {
        public static IApplicationBuilder UseJwt(this IApplicationBuilder builder) => builder.UseMiddleware<JWTDefaultMiddleware>();
    }
}