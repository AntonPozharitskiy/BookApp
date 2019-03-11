using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using BLL.Config;
using BLL.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSecurityToken _jwtToken;
        public TokenService(JwtConfigurationSettings configuration)
        {
            var jwtConfiguration = configuration;
            var issuer = jwtConfiguration.TokenIssuer;
            var secretKey = jwtConfiguration.TokenKey;
            var lifetime = jwtConfiguration.TokenExpireTime;

            _jwtToken = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
                issuer: issuer,
                expires: lifetime,
                signingCredentials: secretKey
                );
        }

        public string GetAuthenticationToken()
        {
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(_jwtToken);
            return encodedToken;
        }
    }
}
