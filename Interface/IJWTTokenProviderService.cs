using JWTLibrary.JWT;

namespace JWTLibrary.Interface
{
    public interface IJWTTokenProviderService
    {
        TokenData RefreshToken(string rToken);
    }
}