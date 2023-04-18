using EventManagement.Application.DTOs;
using EventManagement.Application.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Utilities
{
    public class TokenGenerator<T> where T : class, IAuthenticationService
    {
        protected async Task<string> GenerateJwtTokenAsync(string userEmail, UserDto identityUser, string jwtSecretKey, string tokenAudience, string tokenIssuer)
        {
            //var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, userEmail), new Claim(JwtRegisteredClaimNames.NameId, identityUser.Id.ToString()) };
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, identityUser.UserId.ToString()),
                new Claim(ClaimTypes.Name, identityUser.Name),
            };
            //claims.AddRange(identityUser.Roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            //claims.AddRange(identityUser.RoleId.Select(roleId => new Claim(ClaimsIdentity.DefaultRoleClaimType, roleId.ToString())));

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
            //var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(ApplicationConstants.JwtExpireTime),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var createToken = tokenHandler.CreateToken(tokenDescripter);

            return await Task.Run(() =>
            {
                return tokenHandler.WriteToken(createToken);
            });
        }

        protected string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
