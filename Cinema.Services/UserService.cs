using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using CinemaDB.EF;
using CinemaDB.Entities;
using CinemaServices.Constants;
using CinemaServices.DTOs;
using CinemaServices.Interfaces;

namespace CinemaServices
{
    public class UserService: IUserService
    {
        public UserDTO CreateUser(AuthDTO authDto)
        {
            using var context = new ApplicationContext();

            const int numberOfBytes = 16;
            var salt = CreateSalt(numberOfBytes);
            var hashedPassword = CreateHashedPassword(authDto.Password, salt);
            var user = new UserEntity
            {
                Email = authDto.Email,
                Password = hashedPassword,
                Salt = Encoding.Unicode.GetString(salt),
                Role = "user"
            };
            context.Users.Add(user);
            context.SaveChanges();

            var userDto = new UserDTO
            {
                Id = context.Users.FirstOrDefault(u => u.Email == authDto.Email).Id,
                Email = authDto.Email,
                Role = Roles.User.ToString().ToLower()
            };
            return userDto;
        }

        public UserDTO CheckAuthData(AuthDTO authDto)
        {
            using var context = new ApplicationContext();
            var user = context.Users.FirstOrDefault(u => u.Email == authDto.Email);
            if (user == null)
            {
                throw new Exception("User with this email wasn't found:(");
            }

            var salt = Encoding.Unicode.GetBytes(user.Salt);
            var hashedPassword = CreateHashedPassword(authDto.Password, salt);
            if (user.Password != hashedPassword)
            {
                throw new Exception("Password is incorrect:(");
            }
            var userDto = new UserDTO
            {
                Id = user.Id,
                Email = authDto.Email,
                Role = user.Role
            };
            return userDto;
        }

        private static byte[] CreateSalt(int size)
        {
            var salt = new byte[size];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(salt);
            return salt;
        }

        private static string CreateHashedPassword(string password, byte[] salt)
        {
            return Encoding.Unicode.GetString(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 64
                )
            );
        }
    }
}