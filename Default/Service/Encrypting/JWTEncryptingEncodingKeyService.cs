using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JWTLibrary.Interface.Encrypting;
using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Default.Service.Encrypting
{
    public class JWTEncryptingEncodingKeyService : IJWTEncryptingEncodingKeyService
    {
        public string SigningAlgorithm { get; set; } = JwtConstants.DirectKeyUseAlg;
        public string EncryptingAlgorithm { get; set; } = SecurityAlgorithms.Aes256CbcHmacSha512;

        public SecurityKey GetKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}