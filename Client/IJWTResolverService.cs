using System.Threading.Tasks;
using JWTLibrary.Interface;
using JWTLibrary.JWT;

namespace JWTLibrary.Client
{
    public interface IJWTResolverService
    {
        IJWTLifeTimeOptions JWTLifeTimeOptions { get; set; }
        Task<TokenData> ResolveCode(string code);
        Task<TokenData> RefreshToken(string refreshtoken);
        string GetAuthenticationCodeURL();
    }
}