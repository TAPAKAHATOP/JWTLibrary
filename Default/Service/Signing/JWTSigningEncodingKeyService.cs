using System.Text;
using JWTLibrary.Interface.Signing;
using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Default.Service.Signing
{
    public class JWTSigningEncodingKeyService : IJWTSigningEncodingKeyService
    {
        public string SigningAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256;

        public SecurityKey GetKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}