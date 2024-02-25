using Microsoft.AspNetCore.Mvc;
using WishListApi.Models.DTOs;
using WishListApi.Models;
using WishListApi.Services;

namespace WishListApi.WebAPI.User
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
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 403)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public IActionResult Login([FromBody] LoginDto login)
        {
            try {
                var token = _loginService.Login(login);
                if (token == null) {
                    return Unauthorized(new ErrorModel(){Message = "Wrong login or password", Code = "WrongLoginData"});
                } 

                if (!_loginService.IsUserActive(login.Login)) {
                    return StatusCode(403, new ErrorModel(){Message = "Account is inactive", Code = "InactiveAccount"});
                }

                return Ok(token);
            } catch (Exception Error) {
                Console.WriteLine(Error);
                return StatusCode(500, new ErrorModel(){Message = "Internal server error", Code = "ServerError"});
            }
        }
        
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 409)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            try {
                if(_loginService.IsUserExist(registerDto.Login)) {
                    return Conflict(new ErrorModel(){Message = "User already exists.", Code = "UserExists"});
                }

                if(_loginService.IsEmailExist(registerDto.Email)) {
                    return Conflict(new ErrorModel(){Message = "Email already exists.", Code = "EmailExists"});
                }

                var errors = _loginService.ValidateRegisterFields(registerDto);
                if (errors.Count > 0) {
                    return BadRequest(errors);
                }

                var token = _loginService.Register(registerDto);
                return Created("", token);
            } catch (Exception Error) {
                Console.WriteLine(Error);
                return StatusCode(500, new ErrorModel(){Message = "Internal server error", Code = "ServerError"});
            }
        }

       
    }
}