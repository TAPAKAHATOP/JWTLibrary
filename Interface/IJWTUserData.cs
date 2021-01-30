using System;

namespace JWTLibrary.Interface
{
    public interface IJWTUserData
    {
        string DisplayName { get; set; }
        string Identifier { get; set; }
        bool IsAuthenticated { get; set; }
    }
}