using JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace JWTLibrary.Utils.Options
{
    public class JwtBearerOptionsPostConfigureOptions : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly JwtHandler _tokenValidator;

        public JwtBearerOptionsPostConfigureOptions(JwtHandler tokenValidator)
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
