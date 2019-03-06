using System;
using System.Collections.Generic;
using System.Text;
using BLL.DataAccess;
using BLL.Entities;

namespace BLL.Services
{
    public class UserService
    {
        private IRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;
    }
}
