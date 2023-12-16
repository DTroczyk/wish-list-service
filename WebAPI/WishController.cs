using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WishListApi.Models;
using WishListApi.Services;

namespace WishListApi.WebAPI
{
    [ApiController]
    [Route("api/wish")]
    public class WishController : ControllerBase
    {
        private readonly IWishService _wishService;

        public WishController(IWishService wishService)
        {
            _wishService = wishService;
        }

        [HttpGet("get-own-wishes")]
        public IActionResult GetOwnWishes() {
            try {
                
                return Ok();
            } catch (Exception Error) {
                Console.WriteLine(Error);
                return StatusCode(500, new ErrorModel(){Message = "Internal server error", Code = "ServerError"});
            }
        }
    }
}