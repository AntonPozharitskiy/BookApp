using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Managers;
using Microsoft.AspNetCore.Identity;

namespace BLL.Wrappers
{
    public class RoleManagerWrapper : IRoleManager
    {
        private readonly RoleManager<Role> _manager;

        public RoleManagerWrapper(RoleManager<Role> manager)
        {
            _manager = manager;
        }

        public Task<IdentityResult> RemoveRole(Role roleToRemove)
        {
            return _manager.DeleteAsync(roleToRemove);
        }

        public List<Role> GetAllRolesSync()
        {
            return _manager.Roles.ToList();
        }

        public Task<bool> IsRoleExist(string role)
        {
            return _manager.RoleExistsAsync(role);
        }
    }
}
