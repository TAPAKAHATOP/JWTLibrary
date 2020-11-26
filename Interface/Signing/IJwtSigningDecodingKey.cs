using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Interface.Signing
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey(string clientKey);
    }
}