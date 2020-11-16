using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Interface;
using JWTLibrary.Interface;
using Newtonsoft.Json;

namespace JWTLibrary.JWT
{
    public class TokenResolverService
    {
        public HttpClient Http = new HttpClient();
        public IAuthOptions AuthOptions { get; }

        public TokenResolverService(IAuthOptions opt)
        {
            this.AuthOptions = opt;
        }

        public async Task<TokenData> ResolveCodeAsync(string code, string sign)
        {
            var content = new FormUrlEncodedContent(new[]
                   {
                new KeyValuePair<string, string>("", code)
            });
            var url = $"{this.AuthOptions.Server}cap/code/{this.AuthOptions.ApplicationClientId}/?shareCode={code}&signature={sign}";
            HttpResponseMessage response = await this.Http.PostAsync(url, content);
            try
            {

                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();

                TokenData token = JsonConvert.DeserializeObject<TokenData>(resp);
                return token;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TokenData> RefreshToken(string code, string sign)
        {
            var content = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string, string>("", code)
            });
            try
            {
                var url = $"{this.AuthOptions.Server}cap/refresh/{this.AuthOptions.ApplicationClientId}?code={code}&signature={sign}";
                HttpResponseMessage response = await this.Http.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();

                TokenData token = JsonConvert.DeserializeObject<TokenData>(resp);
                return token;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}