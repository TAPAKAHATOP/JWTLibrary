namespace Interface
{
    public interface IClientApplication
    {
        string GetClientKey();

        string GetAudience();
        string GetPrivateKey();
    }
}