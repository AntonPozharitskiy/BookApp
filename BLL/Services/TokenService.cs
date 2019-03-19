using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Config;
using BLL.Entities;
using BLL.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserManager _userManager;
        public TokenService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public string GetAuthenticationToken(string userEmail)
        {
            var claims = new List<Claim>{ new Claim(JwtRegisteredClaimNames.Sub, userEmail) };
            var jwtToken = new JwtSecurityToken(
                    AuthOptions.ISSUER,
                    AuthOptions.AUDIENCE,
                    claims,
                    expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
           return new JwtSecurityTokenHandler().WriteToken(jwtToken);

        }
    }
}
