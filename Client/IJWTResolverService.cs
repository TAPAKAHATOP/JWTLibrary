using System.Threading.Tasks;
using JWTLibrary.JWT;

namespace JWTLibrary.Client
{
    public interface IJWTResolverService
    {
        Task<TokenData> ResolveCode(string code, string signature);
        Task<TokenData> RefreshToken(string refreshtoken, string signature);
    }
}