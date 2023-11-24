using Microsoft.AspNetCore.Identity;
using WishListApi.Models;
using WishListApi.Models.DTOs;

namespace WishListApi.Services
{
    public interface ILoginService
    {
        public User Register(RegisterDto registerDto);
        public bool UserExist(string login);
    }
}