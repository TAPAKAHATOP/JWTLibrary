using JWTLibrary.JWT;

namespace JWTLibrary.Interface
{
    public interface IJWTokenProviderService
    {
        TokenData RefreshToken(string rToken);
    }
}