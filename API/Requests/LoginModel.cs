﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Entities;

namespace API.Requests
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginModel() { }
    }
}
