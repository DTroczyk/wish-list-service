using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishListApi.Context;
using WishListApi.Models;
using WishListApi.Services;

namespace WishListApi.WebAPI
{
    [ApiController]
    [Route("api/wish")]
    [Authorize]
    public class WishController : ControllerBase
    {
        private readonly IWishService _wishService;
        private readonly ILoginService _loginService;

        public WishController(IWishService wishService, ILoginService loginService)
        {
            _wishService = wishService;
            _loginService = loginService;
        }

        [HttpGet("get-own-active-wishes")]
        [ProducesResponseType(typeof(IEnumerable<WishVm>), 200)]
        [ProducesResponseType(typeof(ErrorModel), 401)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public IActionResult GetOwnActiveWishes()
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var user = _loginService.GetUserByToken(token);

                if (user != null)
                {
                    var wishes = _wishService.GetOwnActiveWishes(user);
                    if (wishes != null)
                    {
                        return Ok(wishes);
                    }
                    else
                    {
                        return Ok(new List<WishVm>());
                    }
                }
                else
                {
                    return StatusCode(404, new ErrorModel() { Message = "User not found", Code = "UserNotFound" });
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error);
                return StatusCode(500, new ErrorModel() { Message = "Internal server error", Code = "ServerError" });
            }
        }
    }
}