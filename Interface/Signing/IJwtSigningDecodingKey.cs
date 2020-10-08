using Microsoft.IdentityModel.Tokens;

namespace Interface.Signing
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey(string clientKey);
    }
}