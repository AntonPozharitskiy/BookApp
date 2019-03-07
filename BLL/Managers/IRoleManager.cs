using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Managers
{
    public interface IRoleManager
    {
        Task<IdentityResult> RemoveRole(Role roleToRemove);
        List<Role> GetAllRolesSync();
        Task<bool> IsRoleExist(string role);
    }
}
