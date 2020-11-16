namespace JWTLibrary.Interface
{
    public interface IAuthOptions
    {
        string Server { get; set; }
        string Issuer { get; set; }
        string PrivateKey { get; set; }
        string ApplicationClientKey { get; set; }
        string ApplicationClientId { get; set; }
    }
}