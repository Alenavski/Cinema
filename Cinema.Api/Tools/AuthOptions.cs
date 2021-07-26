using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CinemaAPI.Tools
{
    public class AuthOptions
    {
        public static string Issuer;
        public static string Audience;
        private static readonly string _key;
        public static int HoursOfTokenLifetime;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new(Encoding.ASCII.GetBytes(_key));
        }
        
        static AuthOptions()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            Issuer = config.GetSection("AuthOptions")["Issuer"];
            Audience = config.GetSection("AuthOptions")["Audience"];
            _key = config.GetSection("AuthOptions")["Key"];
            HoursOfTokenLifetime = int.Parse(config.GetSection("AuthOptions")["HoursOfTokenLifetime"]);
        }
    }
}