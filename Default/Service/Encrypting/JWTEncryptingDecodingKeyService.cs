using System.Text;
using JWTLibrary.Interface.Encrypting;
using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Default.Service.Encrypting
{
    public class JWTEncryptingDecodingKeyService : IJWTEncryptingDecodingKeyService
    {
        public SecurityKey GetKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}