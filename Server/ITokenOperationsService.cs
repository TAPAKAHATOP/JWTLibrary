using JWTLibrary.JWT;

namespace JWTLibrary.Server
{
    public interface ITokenOperationsService
    {
        TokenData TryRefreshTokenData(string appKey, string code, string signature);
        TokenValidationData TryValidateToken(string appKey, string code, string signature);
        TokenValidationData RemoveTokenByAccess(string appKey, string tokenDigest);
        TokenValidationData RemoveTokenByRefresh(string appKey, string token);
    }
}