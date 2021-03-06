using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Api.Tools
{
    public class AuthOptions
    {
        public const string Position = "AuthOptions";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int HoursOfTokenLifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new(Encoding.ASCII.GetBytes(Key));
        }
    }
}