using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JWTLibrary.Interface;
using JWTLibrary.Interface.Encrypting;
using JWTLibrary.Interface.Signing;
using JWTLibrary.Server;
using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Default.Service.Server
{
    public class JWTGeneratorService : JwtSecurityTokenHandler, IJWTGeneratorService
    {
        public IAuthOptions AuthOptions { get; set; }
        public IJWTEncryptingEncodingKeyService EEKService { get; set; }
        public IJWTSigningEncodingKeyService SEKService { get; set; }

        public JWTGeneratorService(IAuthOptions aOptions, IJWTEncryptingEncodingKeyService eekService, IJWTSigningEncodingKeyService sekService)
        {
            this.AuthOptions = aOptions;
            this.EEKService = eekService;
            this.SEKService = sekService;
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
                    this.SEKService.GetKey(clientApplication.GetClientKey()),
                    this.SEKService.SigningAlgorithm),

                encryptingCredentials: new EncryptingCredentials(
                    this.EEKService.GetKey(clientApplication.GetPrivateKey()),
                    this.EEKService.SigningAlgorithm,
                    this.EEKService.EncryptingAlgorithm)
            );

            var encodedJwt = this.WriteToken(jwt);
            return encodedJwt;
        }
    }
}