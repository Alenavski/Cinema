using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Cinema.DB.EF;
using Cinema.DB.Entities;
using Cinema.Services.Constants;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Cinema.Services.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cinema.Services
{
    public class UserService : IUserService
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
            var salt = CreateSalt(_hashingOptions.NumberBytesOfSalt);
            var hashedPassword = CreateHashedPassword(authDto.Password, salt);

            var user = new UserEntity
            {
                Email = authDto.Email,
                Password = hashedPassword,
                Salt = salt,
                Role = Roles.User.ToString()
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = Enum.Parse<Roles>(user.Role)
            };
        }

        public async Task<UserEntity> FindUserByEmail(AuthDto authDto)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == authDto.Email);
        } 

        public UserDto CheckPassword(UserEntity user, AuthDto authDto)
        {
            var salt = user.Salt;
            var hashedPassword = CreateHashedPassword(authDto.Password, salt);

            if (!user.Password.SequenceEqual(hashedPassword))
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = Enum.Parse<Roles>(user.Role)
            };
        }

        private static byte[] CreateSalt(int size)
        {
            var salt = new byte[size];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(salt);
            return salt;
        }

        private byte[] CreateHashedPassword(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                _hashingOptions.IterationCount,
                _hashingOptions.NumberBytesRequestedForHash
            );
        }
    }
}