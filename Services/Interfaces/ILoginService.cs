using Microsoft.AspNetCore.Identity;
using WishListApi.Models.DTOs;
using WishListApi.Models;
using WishListApi.Models.DTOs;

namespace WishListApi.Services
{
    public interface ILoginService
    {
        public string Register(RegisterDto registerDto);
        public List<ErrorModel> ValidateRegisterFields(RegisterDto registerDto);
        public bool IsUserExist(string login);
        public bool IsEmailExist(string email);
        public bool IsUserActive(string login);
        public string Login(LoginDto loginDto);
    }
}