using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WishListApi.Models;
using WishListApi.Models.DTOs;

namespace WishListApi.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly PasswordHasher<User> _PasswordHasher = new PasswordHasher<User>();
        private readonly User _TestUser = new User();

        public LoginService(AppDbContext dbContext) : base(dbContext)
        {
        }

        public string Register(RegisterDto registerDto)
        {

            throw new NotImplementedException();
        }

        public string HashPassword(string password)
        {
            var hashed =_PasswordHasher.HashPassword(_TestUser, password);
            return hashed;
        }

        public PasswordVerificationResult VerifyPassword(string hashedPassword, string password)
        {
            var isVerified = _PasswordHasher.VerifyHashedPassword(_TestUser,hashedPassword,password);
            return isVerified;
        }
    }
}