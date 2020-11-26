using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Interface.Encrypting
{
    public interface IJwtEncryptingDecodingKey
    {
        SecurityKey GetKey(string privateKey);
    }
}