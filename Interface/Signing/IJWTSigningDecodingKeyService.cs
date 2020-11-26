using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Interface.Signing
{
    public interface IJWTSigningDecodingKeyService
    {
        SecurityKey GetKey(string clientKey);
    }
}