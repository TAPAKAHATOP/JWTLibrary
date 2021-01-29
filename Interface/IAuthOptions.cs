namespace JWTLibrary.Interface
{
    public interface IAuthOptions : IJWTLifeTimeOptions
    {
        string AuthServerURL { get; set; }
        string AuthServerCodeResolverURL { get; set; }
        string Issuer { get; set; }
        string PrivateKey { get; set; }
        string ApplicationClientKey { get; set; }

        string GetAuthenticationRedirectURL();
        string GetAuthenticationCodeResolverURL(string code, string signature);
        string GetAuthenticationRefreshURL(string code, string signature);
    }
}