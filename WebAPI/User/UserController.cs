using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace wish_list_service.WebAPI.User
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        [HttpGet("hello-world")]
        public IActionResult Get()
        {
            return Ok(new { Message = "Hello world" });
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto login)
        {
            return Ok(new { login = login.UserName, property1 = login.Password });
        }
    }
}