using Microsoft.AspNetCore.Authorization;
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
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
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
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