using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Interface.Encrypting
{
    public interface IJWTEncryptingDecodingKeyService
    {
        SecurityKey GetKey(string privateKey);
    }
}