using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JWTLibrary.Client;
using JWTLibrary.Interface;
using JWTLibrary.Interface.Encrypting;
using JWTLibrary.Interface.Signing;
using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Utils
{
    public abstract class AJWTHandler : JwtSecurityTokenHandler, IJWTHandler
    {
        public IAuthOptions AuthOptions { get; set; }
        public IJWTEncryptingDecodingKeyService EDKService { get; set; }
        public IJWTSigningDecodingKeyService SDKService { get; set; }

        public AJWTHandler(IAuthOptions authOptions, IJWTEncryptingDecodingKeyService edkService, IJWTSigningDecodingKeyService sdkService)
        {
            this.AuthOptions = authOptions;
            this.EDKService = edkService;
            this.SDKService = sdkService;
        }

        public virtual new ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            //JwtSecurityToken incomingToken = ReadJwtToken(token);
            validationParameters.TokenDecryptionKey = this.EDKService.GetKey(this.AuthOptions.PrivateKey);
            validationParameters.ValidateIssuerSigningKey = true;
            validationParameters.ValidIssuer = this.AuthOptions.Issuer;
            validationParameters.ValidateIssuer = true;
            validationParameters.ValidateAudience = true;
            validationParameters.ValidateLifetime = true;

            validationParameters.IssuerSigningKey = this.SDKService.GetKey(this.AuthOptions.ApplicationClientKey);
            validationParameters.ValidAudience = this.AuthOptions.ApplicationClientKey;

            //And let the framework take it from here.
            return base.ValidateToken(token, validationParameters, out validatedToken);
        }
    }
}