using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using wish_list_service.Models.DTOs;
using WishListApi.Models.DTOs;
using WishListApi.Services;

namespace wish_list_service.WebAPI.User
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public UserController(ILoginService loginService) {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            return Ok(new { login = login.UserName, property1 = login.Password });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                Register(registerDto);
                return NoContent();
            }
            catch (Exception Error)
            {
                return BadRequest(Error);
            }
        }

       
    }
}