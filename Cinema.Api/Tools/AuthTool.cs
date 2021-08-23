using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cinema.Api.Tools.Interfaces;
using Cinema.Services.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Api.Tools
{
    public class AuthTool : IAuthTool
    {
        private readonly AuthOptions _authOptions;
        public AuthTool(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions.Value;
        }

        public string CreateToken(UserDto userDto)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, userDto.Email),
                new(ClaimTypes.Sid, userDto.Id.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, userDto.Role.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimTypes.Email, ClaimTypes.Role);

            var dateNow = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Audience,
                notBefore: dateNow,
                claims: claimsIdentity.Claims,
                expires: dateNow.Add(TimeSpan.FromHours(_authOptions.HoursOfTokenLifetime)),
                signingCredentials: new SigningCredentials(
                    _authOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt); 
        }
    }
}