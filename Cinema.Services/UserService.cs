using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Constants;
using Cinema.Services.Dtos;
using Cinema.Services.Exceptions;
using Cinema.Services.Interfaces;
using Cinema.Services.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cinema.Services
{
    public class UserService: IUserService
    {
        private ApplicationContext _context;
        private HashingOptions _hashingOptions;
        
        public UserService(ApplicationContext context, IOptions<HashingOptions> hashingOptions)
        {
            _context = context;
            _hashingOptions = hashingOptions.Value;
        }

        public async Task<UserDto> CreateUser(AuthDto authDto)
        {
            const int numberOfBytes = 16;
            var salt = CreateSalt(numberOfBytes);
            var hashedPassword = CreateHashedPassword(authDto.Password, salt);
            var user = new UserEntity
            {
                Email = authDto.Email,
                Password = hashedPassword,
                Salt = Encoding.Unicode.GetString(salt),
                Role = Roles.User.ToString()
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Email = authDto.Email,
                Role = Roles.User.ToString().ToLower()
            };
        }

        public async Task<UserDto> CheckAuthData(AuthDto authDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == authDto.Email);
            if (user == null)
            {
                throw new AuthenticationDataInvalidException("User with this email wasn't found:(");
            }

            var salt = Encoding.Unicode.GetBytes(user.Salt);
            var hashedPassword = CreateHashedPassword(authDto.Password, salt);
            if (user.Password != hashedPassword)
            {
                throw new AuthenticationDataInvalidException("Password is incorrect:(");
            }
            return new UserDto
            {
                Id = user.Id,
                Email = authDto.Email,
                Role = user.Role
            };
        }

        private static byte[] CreateSalt(int size)
        {
            var salt = new byte[size];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(salt);
            return salt;
        }

        private string CreateHashedPassword(string password, byte[] salt)
        {
            return Encoding.Unicode.GetString(
                KeyDerivation.Pbkdf2(
                    password,
                    salt,
                    KeyDerivationPrf.HMACSHA1,
                    _hashingOptions.IterationCount,
                    _hashingOptions.NumBytesRequested
                )
            );
        }
    }
}