using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Entities;

namespace API.Requests
{
    public class RequestUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public RequestUserModel() { }
        public static explicit operator User(RequestUserModel userModel)
        {
            return new User
            {
                Email = userModel.Email,
                UserName = userModel.Email
            };
        }
    }
}
