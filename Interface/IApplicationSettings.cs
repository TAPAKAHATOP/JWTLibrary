namespace Interface
{
    public interface IApplicationSettings
    {
        string ApplicationAuthClientKey { get; set; }
        string PrivateKey { get; set; }
    }
}