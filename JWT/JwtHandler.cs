using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Interface;
using Interface.Encrypting;
using Interface.Providers;
using Interface.Signing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JWT
{
    public class JwtHandler : JwtSecurityTokenHandler, ISecurityTokenValidator
    {
        public ILogger<JwtHandler> Logger { get; private set; }
        public JwtHandler(IAuthOptions authOptions, IApplicationSettings applicationSettings, IClientApplicationTokenProviderService clientProvider, IJwtSigningDecodingKey signingDecodingKey, IJwtEncryptingDecodingKey encryptingDecodingKey)
        {
            this.AuthOptions = authOptions;
            this.ApplicationSettings = applicationSettings;
            this.ClientProvider = clientProvider;
            this.SigningDecodingKey = signingDecodingKey;
            this.EncryptingDecodingKey = encryptingDecodingKey;

        }
        public IAuthOptions AuthOptions { get; private set; }
        public IApplicationSettings ApplicationSettings { get; private set; }
        public IClientApplicationTokenProviderService ClientProvider { get; set; }
        public IJwtSigningDecodingKey SigningDecodingKey { get; private set; }
        public IJwtSigningEncodingKey SigningEncodingKey { get; private set; }
        public IJwtEncryptingDecodingKey EncryptingDecodingKey { get; private set; }
        public IJwtEncryptingEncodingKey EncryptingEncodingKey { get; private set; }

        public JwtHandler(
            ILogger<JwtHandler> logger,
            IAuthOptions auOP,
            IApplicationSettings aSettings,
            IClientApplicationTokenProviderService cProvider,
            IJwtSigningDecodingKey signingDecodingKey,
            IJwtSigningEncodingKey signingEncodingKey,
            IJwtEncryptingDecodingKey encryptingDecodingKey,
            IJwtEncryptingEncodingKey encryptingEncodingKey
        )
        {
            //IdentityModelEventSource.ShowPII = true;
            this.Logger = logger;
            this.AuthOptions = auOP;
            this.ApplicationSettings = aSettings;
            this.ClientProvider = cProvider;
            this.SigningDecodingKey = signingDecodingKey;
            this.SigningEncodingKey = signingEncodingKey;
            this.EncryptingDecodingKey = encryptingDecodingKey;
            this.EncryptingEncodingKey = encryptingEncodingKey;
        }

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            JwtSecurityToken incomingToken = ReadJwtToken(token);

            string clientguid = this.ApplicationSettings.ApplicationAuthClientKey;
            IClientApplication clientApp = this.ClientProvider.GetApplicationByClientId(clientguid);

            validationParameters.TokenDecryptionKey = this.EncryptingDecodingKey.GetKey(this.ApplicationSettings.PrivateKey);
            validationParameters.ValidateIssuerSigningKey = true;
            validationParameters.ValidateIssuer = true;
            validationParameters.ValidateAudience = true;
            validationParameters.ValidateLifetime = true;

            if (this.ClientProvider.ValidateTokenForApplication(token, clientguid))
            {
                validationParameters.IssuerSigningKey = SigningDecodingKey.GetKey(ClientProvider.GetApplicationByClientId(clientguid).GetClientKey());
                validationParameters.ValidAudience = clientApp.GetAudience();
            }
            else
            {
                validationParameters.IssuerSigningKey = SigningDecodingKey.GetKey(Guid.Empty.ToString());
                validationParameters.ValidAudience = Guid.Empty.ToString();
            }

            //And let the framework take it from here.
            return base.ValidateToken(token, validationParameters, out validatedToken);
        }

        public string GenerateAccessToken(ClaimsIdentity identity, IClientApplication clientApplication, DateTime from, DateTime to)
        {
            var jwt = this.CreateJwtSecurityToken(
                issuer: AuthOptions.GetIssuer(),
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
                    this.EncryptingEncodingKey.EncryptingAlgorithm));

            var encodedJwt = this.WriteToken(jwt);
            return encodedJwt;
        }
    }
}