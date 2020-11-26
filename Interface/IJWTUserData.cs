using System;

namespace JWTLibrary.Interface
{
    public class IJWTUserData
    {
        public string DisplayName { get; set; }
        public Guid Id { get; set; }
        public bool IsAuthenticated { get; set; }

    }
}