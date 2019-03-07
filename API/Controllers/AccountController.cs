using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Requests;
using API.Responses;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.Entities;
using BLL.Managers;
using DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISignInManager _signInManager;
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;

        public AccountController(ISignInManager signInManager, IUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserModel requestModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newUser = (User) requestModel;
            newUser.Id = Guid.NewGuid();
            await _userManager.CreateUser(newUser, requestModel.Password);
            await _userManager.AddToRole(newUser, "User");
            return Ok(newUser);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task Logout()
        {
            await _signInManager.Logout();
        }
    }
}