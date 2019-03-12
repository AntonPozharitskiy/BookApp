using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Requests;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.Entities;
using BLL.Managers;
using DAL.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("Account")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISignInManager _signInManager;
        private readonly IUserManager _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(ISignInManager signInManager, IUserManager userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserModel registerModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newUser = (User) registerModel;
            newUser.Id = Guid.NewGuid();
            await _userManager.CreateUser(newUser, registerModel.Password);
            await _userManager.AddToRole(newUser, "User");
            return Ok(newUser);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<object> Authenticate(LoginModel loginModel)
        {
            var currentUser = _userManager.GetUserByEmail(loginModel.Email);
            if(currentUser == null) return BadRequest("There are not users with this email");
            var foundedUser = (User)loginModel;
            await _signInManager.CheckPassword(foundedUser, loginModel.Password, false);
            var authToken = new
            {
                accessToken = _tokenService.GetAuthenticationToken(),
                userEmail = loginModel.Email
            };
            return authToken;
        }

        [HttpPost]
        [Route("Logout")]
        public async Task Logout()
        {
            await _signInManager.Logout();
        }
    }
}