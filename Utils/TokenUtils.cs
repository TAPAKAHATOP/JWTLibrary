using System.Security.Cryptography;
using System.Text;

namespace JWTLibrary.Utils
{
    public class TokenUtils
    {
        public string GetMD5Digest(string data)
        {
            using (MD5 md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}