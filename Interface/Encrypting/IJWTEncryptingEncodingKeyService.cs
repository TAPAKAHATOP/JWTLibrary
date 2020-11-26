using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Interface.Encrypting
{
    public interface IJWTEncryptingEncodingKeyService
    {
        string SigningAlgorithm { get; set; }
        string EncryptingAlgorithm { get; set; }

        SecurityKey GetKey(string key);
    }
}