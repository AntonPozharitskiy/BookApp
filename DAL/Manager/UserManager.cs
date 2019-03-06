using System;
using System.Collections.Generic;
using System.Text;
using BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Manager
{
    class UserManager : Finder<User>
    {
        public UserManager(DbSet<User> entity) : base(entity)
        {
        }
    }
}
