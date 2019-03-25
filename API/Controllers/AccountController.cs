using System;
using System.Threading.Tasks;
using API.Mapping;
using API.Requests;
using API.Responses;
using AutoMapper;
using BLL;
using Microsoft.AspNetCore.Mvc;
using BLL.Entities;
using BLL.Managers;
using Microsoft.AspNetCore.Cors;

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
        public async Task<ActionResult> Register(RequestUserModel registerModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var mappedUser = Mapper.Map<RequestUserModel, User>(registerModel);
            mappedUser.Id = Guid.NewGuid();
            await _userManager.CreateUser(mappedUser, registerModel.Password);
            await _userManager.AddToRole(mappedUser, "User");
            return Ok(mappedUser);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<object> Authenticate(RequestUserModel loginModel)
        {
            var currentUser = await _userManager.GetUserByEmail(loginModel.Email);
            if(currentUser == null) return BadRequest("There are not users with this email");
            Mapper.Map(loginModel, currentUser);
            var passconfirm = await _signInManager.CheckPassword(currentUser, loginModel.Password, false);
            var authToken = new
            {
                accessToken = _tokenService.GetAuthenticationToken(currentUser.Email),
                userEmail = currentUser.Email,
                id = currentUser.Id
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