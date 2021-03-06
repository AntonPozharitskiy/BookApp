﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL
{
    public interface IUserManager
    {
        Task<IdentityResult> CreateUser(User user, string password);
        Task<IdentityResult> AddToRole(User user, string role);
        Task<User> GetUserByEmail(string email);
        Task<IList<String>> GetUserRoles(User user);
    }
}
