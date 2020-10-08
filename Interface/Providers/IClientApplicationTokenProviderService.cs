namespace Interface.Providers
{
    public interface IClientApplicationTokenProviderService
    {
        IClientApplication GetApplicationByClientId(string clientguid);
        bool ValidateTokenForApplication(string token, string clientguid);
    }
}