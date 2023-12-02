using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
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
            try {
                Console.WriteLine(login);
                var user = _loginService.Login(login);
                if (user == null) {
                    return BadRequest("Wrong data");
                } else {
                    return Ok(user);
                }
            } catch (Exception Error) {
                return Unauthorized(Error.Message);
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if(_loginService.UserExist(registerDto.Login)) {
                return Conflict("User already exists.");
            }

            var errors = _loginService.ValidateRegisterFields(registerDto);
            if (errors.Count > 0) {
                return BadRequest(new {message = "Fields are incorrect.", errors});
            }

            var newUser = _loginService.Register(registerDto);
            return Created("", newUser);
        }

       
    }
}