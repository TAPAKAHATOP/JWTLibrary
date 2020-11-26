using System.Text;
using JWTLibrary.Interface.Signing;
using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Default.Service.Signing
{
    public class JWTSigningDecodingKeyService : IJWTSigningDecodingKeyService
    {
        public SecurityKey GetKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}