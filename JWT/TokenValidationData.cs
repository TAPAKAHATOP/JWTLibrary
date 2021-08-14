using JWTLibrary.Utils;

namespace JWTLibrary.JWT
{
    public class TokenValidationData
    {
        public TokenStatus Status { get; set; }
        public TokenValidationData()
        {
            this.Status = TokenStatus.INVALID;
        }
    }
}