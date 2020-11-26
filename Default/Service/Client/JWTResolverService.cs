using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JWTLibrary.Client;
using JWTLibrary.Interface;
using JWTLibrary.JWT;
using Newtonsoft.Json;

namespace JWTLibrary.Default.Service.Client
{
    public class JWTResolverDefaultService : IJWTResolverService
    {
        public HttpClient Http = new HttpClient();
        public IAuthOptions AuthOptions { get; }

        public JWTResolverDefaultService(IAuthOptions opt)
        {
            AuthOptions = opt;
        }

        public async Task<TokenData> ResolveCode(string code, string sign)
        {
            var content = new FormUrlEncodedContent(new[]
                   {
                new KeyValuePair<string, string>("", code)
            });
            var url = $"{AuthOptions.Server}cap/code/{AuthOptions.ApplicationClientId}/?shareCode={code}&signature={sign}";
            HttpResponseMessage response = await Http.PostAsync(url, content);
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
                var url = $"{AuthOptions.Server}cap/refresh/{AuthOptions.ApplicationClientId}?code={code}&signature={sign}";
                HttpResponseMessage response = await Http.PostAsync(url, content);
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