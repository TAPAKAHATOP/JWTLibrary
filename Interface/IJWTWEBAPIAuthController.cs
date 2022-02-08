using JWTLibrary.JWT;

namespace JWTLibrary.Interface
{
    public interface IJWTWEBAPITokenController
    {
        TokenData Refresh(string appKey, string code, string signature);
        TokenValidationData Validate(string appKey, string code, string signature);
        TokenValidationData LogoutByAccess(string appKey, string code);
        TokenValidationData LogoutByRefresh(string appKey, string code);
    }
}