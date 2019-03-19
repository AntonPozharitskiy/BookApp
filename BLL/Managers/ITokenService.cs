using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Config;

namespace BLL.Managers
{
    public interface ITokenService
    {
        string GetAuthenticationToken(string userEmail);
    }
}
