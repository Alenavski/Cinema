using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Tools
{
    public class AuthOptions
    {
        public const string ISSUER = "Cinema";
        public const string AUDIENCE = "Cinema";
        private const string KEY = "ThisKeyIsNotASecretKey";
        public const int LIFETIME = 24;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}