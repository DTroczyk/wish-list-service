using Microsoft.AspNetCore.Mvc;
using wish_list_service.Models.DTOs;
using WishListApi.Models;
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
                    return BadRequest(new ErrorModel(){Message = "Wrong data", Code = "WrongData"});
                } 

                if (!_loginService.IsUserActive(login.Login)) {
                    return StatusCode(403, new ErrorModel(){Message = "Account is inactive", Code = "InactiveAccount"});
                }

                return Ok(user);
            } catch (Exception Error) {
                Console.WriteLine(Error);
                return StatusCode(500, new ErrorModel(){Message = "Internal server error", Code = "ServerError"});
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
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

                var newUser = _loginService.Register(registerDto);
                return Created("", newUser);
            } catch (Exception Error) {
                Console.WriteLine(Error);
                return StatusCode(500, new ErrorModel(){Message = "Internal server error", Code = "ServerError"});
            }
        }

       
    }
}