using System.Threading.Tasks;
using JWTLibrary.Interface;
using JWTLibrary.JWT;

namespace JWTLibrary.Client
{
    public interface IJWTResolverService
    {
        IJWTLifeTimeOptions JWTLifeTimeOptions { get; set; }
        Task<TokenData> ResolveCode(string code, string signature);
        Task<TokenData> RefreshToken(string refreshtoken, string signature);
        string GetAuthenticationCodeURL();
    }
}