using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Requests;
using API.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.Entities;
using DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
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
            var result = await _userManager.CreateAsync(newUser);
            if (!result.Succeeded)
            {
                return BadRequest("Error register user!");
            }

            return Ok(newUser);
        }
    }
}