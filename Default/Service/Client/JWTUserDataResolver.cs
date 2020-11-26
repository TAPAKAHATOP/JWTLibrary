using System;
using System.Security.Claims;
using JWTLibrary.Interface;

namespace JWTLibrary.Default.Service.Client
{
    public class JWTUserDataResolver : IJWTUserDataResolver
    {
        public IJWTUserData ResolveCurrentUserData(ClaimsPrincipal user)
        {
            JWTUserData result = new JWTUserData();
            foreach (var claim in ((ClaimsIdentity)user.Identity).Claims)
            {
                switch (claim.Type)
                {
                    case "name":
                        result.DisplayName = claim.Value;
                        break;
                    case "id":
                        result.Id = Guid.Parse(claim.Value);
                        break;
                }
            }

            result.IsAuthenticated = user.Identity.IsAuthenticated;
            return result;
        }
    }
}