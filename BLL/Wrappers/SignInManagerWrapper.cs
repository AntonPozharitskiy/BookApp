using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Entities;
using BLL.Managers;
using System.Threading.Tasks;

namespace BLL.Wrappers
{
    public class SignInManagerWrapper : ISignInManager
    {
        private readonly SignInManager<User> _signInManager;

        public SignInManagerWrapper(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> CheckPassword(User user, string password, bool lockoutOnFailure)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
