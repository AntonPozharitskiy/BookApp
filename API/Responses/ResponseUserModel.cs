using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Entities;

namespace API.Responses
{
    public class ResponseUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public ResponseUserModel()
        {

        }

        public static explicit operator User(ResponseUserModel userModel)
        {
            return new User
            {
                Email = userModel.Email,
                UserName = userModel.Email
            };
        }
    }
}
