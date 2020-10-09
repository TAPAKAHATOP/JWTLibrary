namespace Interface
{
    public interface IAuthOptions
    {
        string Issuer { get; set; }
        string PrivateKey { get; set; }
        string ApplicationClientKey { get; set; }
        string ApplicationClientId { get; set; }
    }
}