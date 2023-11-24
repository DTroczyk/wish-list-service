using Microsoft.AspNetCore.Identity;
using WishListApi.Models.DTOs;

namespace WishListApi.Services
{
    public interface ILoginService
    {
        public string Register(RegisterDto registerDto);
    }
}