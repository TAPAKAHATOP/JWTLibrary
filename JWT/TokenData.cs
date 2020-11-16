namespace JWTLibrary.JWT
{
    public class TokenData
    {
        public static string Access { get; internal set; } = "atkn";
        public static string Refresh { get; internal set; } = "rtkn";
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}