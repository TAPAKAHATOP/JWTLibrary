using Microsoft.IdentityModel.Tokens;

namespace Interface.Signing
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; set; }

        SecurityKey GetKey(string key);
    }
}