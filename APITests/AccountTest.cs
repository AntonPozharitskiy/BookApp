using System;
using System.Threading.Tasks;
using API.Controllers;
using API.Requests;
using API.Responses;
using BLL;
using BLL.Entities;
using BLL.Managers;
using BLL.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace APITests
{
    public class AccountTest
    {
        private static Mock<IUserManager> mockedUserManager = new Mock<IUserManager>();

        private static Mock<ISignInManager> mockedSignInManager = new Mock<ISignInManager>();

        public AccountController controller = new AccountController(mockedSignInManager.Object, mockedUserManager.Object);

        [Fact]
        public async Task RegisterNewUser()
        {
            RegisterUserModel userModel = new RegisterUserModel { Email = "toxa@gmail.com", Password = "Peijer123_" };
            var result = await controller.Register(userModel);
            
            Assert.IsAssignableFrom<ActionResult>(result);
            Assert.NotNull(result);
            mockedUserManager.Verify(user => user.CreateUser(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Logout()
        {
            await controller.Logout();
            mockedSignInManager.Verify(log => log.Logout(), Times.Once);
        }

    }
}