using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Interface;
using Interface.Encrypting;
using Interface.Signing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JWT
{
    public class JwtHandler : JwtSecurityTokenHandler, ISecurityTokenValidator
    {
        public IAuthOptions AuthOptions { get; private set; }
        public IJwtSigningDecodingKey SigningDecodingKey { get; private set; }
        public IJwtSigningEncodingKey SigningEncodingKey { get; private set; }
        public IJwtEncryptingDecodingKey EncryptingDecodingKey { get; private set; }
        public IJwtEncryptingEncodingKey EncryptingEncodingKey { get; private set; }

        public JwtHandler(
            IAuthOptions auOP,
            IJwtSigningDecodingKey signingDecodingKey,
            IJwtSigningEncodingKey signingEncodingKey,
            IJwtEncryptingDecodingKey encryptingDecodingKey,
            IJwtEncryptingEncodingKey encryptingEncodingKey
        )
        {
            //IdentityModelEventSource.ShowPII = true;
            this.AuthOptions = auOP;
            this.SigningDecodingKey = signingDecodingKey;
            this.SigningEncodingKey = signingEncodingKey;
            this.EncryptingDecodingKey = encryptingDecodingKey;
            this.EncryptingEncodingKey = encryptingEncodingKey;
        }

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            JwtSecurityToken incomingToken = ReadJwtToken(token);

            validationParameters.TokenDecryptionKey = this.EncryptingDecodingKey.GetKey(this.AuthOptions.PrivateKey);
            validationParameters.ValidateIssuerSigningKey = true;
            validationParameters.ValidateIssuer = true;
            validationParameters.ValidateAudience = true;
            validationParameters.ValidateLifetime = true;

            validationParameters.IssuerSigningKey = SigningDecodingKey.GetKey(this.AuthOptions.ApplicationClientKey);
            validationParameters.ValidAudience = this.AuthOptions.ApplicationClientId;

            //And let the framework take it from here.
            return base.ValidateToken(token, validationParameters, out validatedToken);
        }

        public string GenerateAccessToken(ClaimsIdentity identity, IClientApplication clientApplication, DateTime from, DateTime to)
        {
            var jwt = this.CreateJwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: clientApplication.GetAudience(),
                notBefore: from,
                subject: identity,
                expires: to,
                issuedAt: DateTime.Now,

                signingCredentials: new SigningCredentials(
                    this.SigningEncodingKey.GetKey(clientApplication.GetClientKey()),
                    this.SigningEncodingKey.SigningAlgorithm),

                encryptingCredentials: new EncryptingCredentials(
                    this.EncryptingEncodingKey.GetKey(clientApplication.GetPrivateKey()),
                    this.EncryptingEncodingKey.SigningAlgorithm,
                    this.EncryptingEncodingKey.EncryptingAlgorithm)
            );

            var encodedJwt = this.WriteToken(jwt);
            return encodedJwt;
        }
    }
}