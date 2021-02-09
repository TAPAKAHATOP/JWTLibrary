using System.Threading.Tasks;
using JWTLibrary.Client;
using JWTLibrary.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JWTLibrary.Utils.Middlewares
{
    public abstract class ABaseJWTMiddleware<TMidleware>
    {
        protected const string SIGNATURE = "sign";
        public ABaseJWTMiddleware(ILogger<TMidleware> logger, RequestDelegate next, IJWTResolverService tService, IJWTLifeTimeOptions jwtOptions)
        {
            this.Logger = logger;
            _next = next;
            this.TService = tService;
            this.JwtOptions = jwtOptions;
        }

        public ILogger<TMidleware> Logger { get; }
        public readonly RequestDelegate _next;
        public readonly IJWTResolverService TService;
        public readonly IJWTLifeTimeOptions JwtOptions;

        public virtual async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Xss-Protection", "1");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            await _next(context);
        }
    }
}