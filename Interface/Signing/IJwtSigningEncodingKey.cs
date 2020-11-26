using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Interface.Signing
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; set; }

        SecurityKey GetKey(string key);
    }
}