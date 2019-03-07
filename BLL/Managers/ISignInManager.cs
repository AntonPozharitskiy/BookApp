using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Managers
{
    public interface ISignInManager
    {
        Task<SignInResult> CheckPassword(User user, string password, bool lockoutOnFailure);
        Task Logout();
    }
}
