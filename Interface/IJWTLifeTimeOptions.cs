namespace JWTLibrary.Interface
{
    public interface IJWTLifeTimeOptions
    {
        int RefreshTokenLifeTime { get; set; }
        int AccessTokenLifeTime { get; set; }
    }
}