using System;
using JWTLibrary.Client;
using JWTLibrary.Default.Service;
using JWTLibrary.Default.Service.Client;
using JWTLibrary.Default.Service.Encrypting;
using JWTLibrary.Default.Service.Signing;
using JWTLibrary.Interface.Encrypting;
using JWTLibrary.Interface.Signing;
using JWTLibrary.Utils.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JWTLibrary.Utils
{
    public static class JWTServiceCollectionExtension
    {
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, bool useDefaults = true)
        {
            if (useDefaults)
            {
                services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, JwtBearerOptionsPostConfigureOptions>();
                services.AddSingleton<IJWTEncryptingDecodingKeyService, JWTEncryptingDecodingKeyService>();
                services.AddSingleton<IJWTSigningDecodingKeyService, JWTSigningDecodingKeyService>();
                services.AddSingleton<IJWTResolverService, JWTResolverDefaultService>();
                services.AddSingleton<IJWTHandler, JWTHandler>();
            }


            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                });

            return services;
        }
    }
}