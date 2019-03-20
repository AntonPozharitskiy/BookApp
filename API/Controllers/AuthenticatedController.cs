using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatedController : ControllerBase
    {
        public string GetCurrentUserEmail()
        {
            var identity = (ClaimsIdentity) this.User.Identity;
            var userEmail = identity.FindFirst(JwtRegisteredClaimNames.Sub).Value;
            return userEmail;
        }
    }
}