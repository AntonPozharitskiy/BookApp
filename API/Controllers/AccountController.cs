using System;
using System.Threading.Tasks;
using API.Requests;
using AutoMapper;
using BLL;
using Microsoft.AspNetCore.Mvc;
using BLL.Entities;
using BLL.Managers;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("Account")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ISignInManager _signInManager;
        private readonly IUserManager _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(ISignInManager signInManager, IUserManager userManager, ITokenService tokenService, ILogger<AccountController> logger)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RequestRegisterUserModel registerModel)
        {
            _logger.LogTrace($"Register method called by {registerModel.Email} started...");
            var mappedUser = Mapper.Map<RequestRegisterUserModel, User>(registerModel);
            mappedUser.Id = Guid.NewGuid();
            await _userManager.CreateUser(mappedUser, registerModel.Password);
            await _userManager.AddToRole(mappedUser, "User");
            _logger.LogTrace($"Register method finish successfully. Added new user: id - {mappedUser.Id}, Email - {mappedUser.Email}, Password - {registerModel.Password}");
            return Ok(mappedUser);
        }

        [HttpPost]
        [Route("Login")]
        public object Authenticate(RequestAuthorizeUserModel loginModel)
        {
            _logger.LogTrace($"Login method called by {loginModel.Email} started...");
            var authToken = new
            {
                accessToken = _tokenService.GetAuthenticationToken(loginModel.Email),
                userEmail = loginModel.Email

            };
            _logger.LogTrace($"Login method for user {loginModel.Email} finish successfully!");
            return authToken;
        }

        [HttpPost]
        [Route("Logout")]
        public async Task Logout()
        {
            await _signInManager.Logout();
            _logger.LogTrace("User log out...");
        }
    }
}