using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace CinemaAPI.Tools
{
    public static class AuthTool
    {
        public static string CreateToken(string email, int id, string role)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Sid, id.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimTypes.Email, ClaimTypes.Role);
            var dateNow = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: dateNow,
                claims: claimsIdentity.Claims,
                expires: dateNow.Add(TimeSpan.FromHours(AuthOptions.HoursOfTokenLifetime)),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256
                )
            );
            
            return new JwtSecurityTokenHandler().WriteToken(jwt); 
        }
    }
}