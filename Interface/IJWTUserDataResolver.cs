using System.Security.Claims;

namespace JWTLibrary.Interface
{
    public interface IJWTUserDataResolver
    {
        IJWTUserData ResolveCurrentUserData(ClaimsPrincipal user, IJWTUserData uData);
    }
}