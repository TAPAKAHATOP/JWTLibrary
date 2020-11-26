using System;
using System.Security.Claims;
using JWTLibrary.Interface;

namespace JWTLibrary.Server
{
    public interface IJWTGeneratorService
    {
        string GenerateAccessToken(ClaimsIdentity identity, IClientApplication clientApplication, DateTime from, DateTime to);
    }
}