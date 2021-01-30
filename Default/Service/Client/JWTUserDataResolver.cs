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
                        uData.DisplayName = claim.Value;
                        break;
                    case "id":
                        uData.Identifier = claim.Value;
                        break;
                }
            }

            uData.IsAuthenticated = user.Identity.IsAuthenticated;
            return uData;
        }
    }
}