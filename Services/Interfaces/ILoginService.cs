using Microsoft.AspNetCore.Identity;
using wish_list_service.Models.DTOs;
using WishListApi.Models;
using WishListApi.Models.DTOs;

namespace WishListApi.Services
{
    public interface ILoginService
    {
        public UserVm Register(RegisterDto registerDto);
        public List<ErrorModel> ValidateRegisterFields(RegisterDto registerDto);
        public bool IsUserExist(string login);
        public bool IsEmailExist(string email);
        public bool IsUserActive(string login);
        public UserVm Login(LoginDto loginDto);
    }
}