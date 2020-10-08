using Microsoft.IdentityModel.Tokens;

namespace Interface.Encrypting
{
    public interface IJwtEncryptingDecodingKey
    {
        SecurityKey GetKey(string privateKey);
    }
}