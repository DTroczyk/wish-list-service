using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wish_list_service.WebAPI.User
{
    public class LoginDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}