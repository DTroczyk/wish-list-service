using Microsoft.AspNetCore.Identity;
using wish_list_service.Models.DTOs;
using WishListApi.Models;
using WishListApi.Models.DTOs;

namespace WishListApi.Services
{
    public interface ILoginService
    {
        public User Register(RegisterDto registerDto);
        public List<ErrorModel> ValidateRegisterFields(RegisterDto registerDto);
        public bool UserExist(string login);
        public User Login(LoginDto loginDto);
    }
}