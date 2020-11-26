using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Interface.Signing
{
    public interface IJWTSigningEncodingKeyService
    {
        string SigningAlgorithm { get; set; }

        SecurityKey GetKey(string key);
    }
}