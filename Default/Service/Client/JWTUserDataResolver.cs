using System;
using System.Security.Claims;
using JWTLibrary.Interface;

namespace JWTLibrary.Default.Service.Client
{
    public class JWTUserDataResolver : IJWTUserDataResolver
    {
        public IJWTUserData ResolveCurrentUserData(ClaimsPrincipal user, IJWTUserData uData)
        {

            foreach (var claim in ((ClaimsIdentity)user.Identity).Claims)
            {
                switch (claim.Type)
                {
                    case "name":
                        uData.SetDisplayName(claim.Value);
                        break;
                    case "id":
                        uData.SetIdentifier(claim.Value);
                        break;
                }
            }

            uData.SetIsAuthenticated(user.Identity.IsAuthenticated);
            return uData;
        }
    }
}