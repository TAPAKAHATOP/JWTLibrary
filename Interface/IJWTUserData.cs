using System;
using System.Collections.Generic;

namespace JWTLibrary.Interface
{
    public interface IJWTUserData
    {
        string DisplayName { get; set; }
        string Identifier { get; set; }
        bool IsAuthenticated { get; set; }
        IList<string> Roles { get; set; }
    }
}