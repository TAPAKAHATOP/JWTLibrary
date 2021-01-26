using System;

namespace JWTLibrary.Interface
{
    public interface IJWTUserData
    {
        string GetDisplayName();
        void SetDisplayName(string newDisplayName);
        string GetIdentifier();
        void SetIdentifier(string newId);
        bool IsAuthenticated();
        void SetIsAuthenticated(bool newAuthFlag);
    }
}