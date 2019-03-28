using System;
using System.Threading.Tasks;
using API.Controllers;
using API.Requests;
using BLL;
using BLL.Entities;
using BLL.Managers;
using BLL.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace APITests
{
    public class AccountTest
    {
        private static Mock<IUserManager> mockedUserManager = new Mock<IUserManager>();

        private static Mock<ISignInManager> mockedSignInManager = new Mock<ISignInManager>();

        private static Mock<ITokenService> mockedTokenService = new Mock<ITokenService>();

        private static Mock<ILogger<AccountController>> mockedLogger = new Mock<ILogger<AccountController>>();

        public AccountController controller = new AccountController(mockedSignInManager.Object, mockedUserManager.Object, mockedTokenService.Object, mockedLogger.Object);

        [Fact]
        public async Task RegisterNewUser()
        {
            RequestRegisterUserModel userModel = new RequestRegisterUserModel { Email = "toxa@gmail.com", Password = "Peijer123_" };
            var result = await controller.Register(userModel);
            
            Assert.IsAssignableFrom<ActionResult>(result);
            Assert.NotNull(result);
            mockedUserManager.Verify(user => user.CreateUser(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public object Authenticate()
        {
            RequestAuthorizeUserModel userLoginModel = new RequestAuthorizeUserModel(){Email = "microchel11@efrem.com", Password = "Confirmed123$"};
            var result = controller.Authenticate(userLoginModel);
            Assert.NotNull(result);
            mockedTokenService.Verify(user => user.GetAuthenticationToken(userLoginModel.Email), Times.Once);
            return result;
        }

        [Fact]
        public async Task Logout()
        {
            await controller.Logout();
            
            mockedSignInManager.Verify(log => log.Logout(), Times.Once);
        }

    }
}
