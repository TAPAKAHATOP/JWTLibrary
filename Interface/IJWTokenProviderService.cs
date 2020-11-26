using JWTLibrary.JWT;

namespace JWTLibrary.Interface
{
    public interface IJWTProviderService
    {
        TokenData RefreshToken(string rToken);
    }
}