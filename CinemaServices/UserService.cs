using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using CinemaDB.EF;
using CinemaDB.Entities;
using CinemaServices.ModelsDTO;

namespace CinemaServices
{
    public class UserService
    {
        public AuthDTO CreateUser(AuthDTO authDto)
        {
            using var context = new ApplicationContext();

            var salt = CreateSalt(16);
            var hashedPassword = Encoding.Unicode.GetString(KeyDerivation.Pbkdf2(
                authDto.Password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                64
            ));
            var user = new User()
            {
                Email = authDto.Email,
                Password = hashedPassword,
                Salt = Encoding.Unicode.GetString(salt),
                Role = "user"
            };
            context.Users.Add(user);
            context.SaveChanges();

            authDto.Id = context.Users.FirstOrDefault(u => u.Email == authDto.Email)!.Id;
            authDto.Role = "user";
            return authDto;
        }

        public AuthDTO CheckAuthData(AuthDTO authDto)
        {
            using var context = new ApplicationContext();
            var user = context.Users.FirstOrDefault(u => u.Email == authDto.Email);
            if (user == null)
                throw new Exception("User with this email wasn't found:(");

            var salt = Encoding.Unicode.GetBytes(user.Salt);
            var hashedPassword = Encoding.Unicode.GetString(KeyDerivation.Pbkdf2(
                authDto.Password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                64
            ));
            if (user.Password != hashedPassword)
                throw new Exception("Password is incorrect:(");
            else
            {
                authDto.Role = user.Role;
                authDto.Id = user.Id;
            }
            return authDto;
        }

        private static byte[] CreateSalt(int size)
        {
            var salt = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }
    }
}