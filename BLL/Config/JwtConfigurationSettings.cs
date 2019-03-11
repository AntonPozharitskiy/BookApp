using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Config
{
    public class JwtConfigurationSettings
    {
        private readonly IConfiguration _configuration;


        public JwtConfigurationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SigningCredentials TokenKey => new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtConfigurations:JwtKey"])), SecurityAlgorithms.HmacSha256Signature);
        public string TokenIssuer => _configuration["JwtConfigurations:JwtIssuer"];

        public DateTime TokenExpireTime =>
            DateTime.UtcNow.Add(TimeSpan.FromMinutes(int.Parse(_configuration["JwtConfigurations:JwtExpireDays"])));
    }
}
