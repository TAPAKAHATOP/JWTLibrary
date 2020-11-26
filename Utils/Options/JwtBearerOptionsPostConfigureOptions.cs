using JWTLibrary.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace JWTLibrary.Utils.Options
{
    public class JwtBearerOptionsPostConfigureOptions : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly IJWTHandler _tokenValidator;

        public JwtBearerOptionsPostConfigureOptions(IJWTHandler tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }

        public void PostConfigure(string name, JwtBearerOptions options)
        {
            options.SecurityTokenValidators.Clear();
            options.SecurityTokenValidators.Add(_tokenValidator);
        }
    }
}
