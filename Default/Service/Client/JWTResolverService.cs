using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JWTLibrary.Client;
using JWTLibrary.Interface;
using JWTLibrary.JWT;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JWTLibrary.Default.Service.Client
{
    public class JWTResolverDefaultService : IJWTResolverService
    {
        public HttpClient Http = new HttpClient();

        public ILogger<JWTResolverDefaultService> Logger { get; }
        public IAuthOptions AuthOptions { get; }
        public IJWTLifeTimeOptions JWTLifeTimeOptions { get; set; }

        public JWTResolverDefaultService(ILogger<JWTResolverDefaultService> logger, IAuthOptions opt, IJWTLifeTimeOptions lftOpt)
        {
            this.Logger = logger;
            AuthOptions = opt;
            this.JWTLifeTimeOptions = lftOpt;
        }

        public async Task<TokenData> ResolveCode(string code)
        {
            var content = new FormUrlEncodedContent(new[]
                   {
                new KeyValuePair<string, string>("", code)
            });
            var url = this.AuthOptions.GetAuthenticationCodeResolverURL(code);
            this.Logger.LogInformation("Start resolving share code");

            HttpResponseMessage response = await Http.PostAsync(url, content);
            try
            {
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();

                TokenData token = JsonConvert.DeserializeObject<TokenData>(resp);
                this.Logger.LogInformation("Share code resolving sucsesful");
                return token;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, "Error on resolving auth code");
                return null;
            }
        }

        public async Task<TokenData> RefreshToken(string code)
        {
            this.Logger.LogInformation("Start refreshing user access token");
            try
            {
                var url = this.AuthOptions.GetAuthenticationRefreshURL(code);
                HttpResponseMessage response = await Http.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();

                TokenData token = JsonConvert.DeserializeObject<TokenData>(resp);

                this.Logger.LogInformation("User access token refreshing successful");
                return token;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, "Error on refresh user token");
                return null;
            }
        }

        public string GetAuthenticationCodeURL()
        {
            return this.AuthOptions.GetAuthenticationRedirectURL();
        }
    }
}