using JWTLibrary.Interface;
using JWTLibrary.Interface.Encrypting;
using JWTLibrary.Interface.Signing;
using JWTLibrary.Utils;

namespace JWTLibrary.Default.Service
{
    public class JWTHandler : AJWTHandler
    {
        public JWTHandler(IAuthOptions authOptions, IJWTEncryptingDecodingKeyService edkService, IJWTSigningDecodingKeyService sdkService)
        : base(authOptions, edkService, sdkService)
        {
        }
    }
}