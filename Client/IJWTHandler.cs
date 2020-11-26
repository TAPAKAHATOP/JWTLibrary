using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace JWTLibrary.Client
{
    public interface IJWTHandler : ISecurityTokenValidator
    {
    }
}