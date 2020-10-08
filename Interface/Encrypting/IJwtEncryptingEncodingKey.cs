using Microsoft.IdentityModel.Tokens;

namespace Interface.Encrypting
{
    public interface IJwtEncryptingEncodingKey
    {
        string SigningAlgorithm { get; set; }
        string EncryptingAlgorithm { get; set; }

        SecurityKey GetKey(string v);
    }
}