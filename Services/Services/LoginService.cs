using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WishListApi.Models;
using WishListApi.Models.DTOs;
using wish_list_service.Models.DTOs;

namespace WishListApi.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly PasswordHasher<User> _PasswordHasher = new PasswordHasher<User>();

        public LoginService(AppDbContext dbContext) : base(dbContext)
        {
        }

        public bool UserExist(string login) {
            var isUserExist = _dbContext.User.FirstOrDefault(u => u.Login == login);
            return isUserExist != null;
        }

        private IList<string> ValidateRegisterFields(RegisterDto registerDto) {
            var errors = new List<string>();
            return errors;
        }

        public User Register(RegisterDto registerDto)
        {
            IList<string> errors = ValidateRegisterFields(registerDto);

            if (errors.Count > 0) {
                throw new Exception("Fields are incorrect.");
            }

            User newUser = new User();
                newUser.FirstName = registerDto.FirstName;
            newUser.LastName = registerDto.LastName;
            newUser.Email = registerDto.Email;
            newUser.Login = registerDto.Login;
            newUser.Password = _PasswordHasher.HashPassword(newUser,registerDto.Password);
            newUser.IsActive = false;
            newUser.RegisterDate = DateTime.UtcNow;
            Console.WriteLine(newUser.Login);

            _dbContext.User.Add(newUser);
            _dbContext.SaveChanges();
            var addedUser = _dbContext.User.Find(registerDto.Login);
           
           if (addedUser != null) {
            return addedUser;
           }
           else {
            throw new Exception("Object cannot be get.");
           }
        }

        public User Login(LoginDto loginDto)
        {
            var user = _dbContext.User.Find(loginDto.Login) ?? throw new Exception("User not exist.");

            PasswordVerificationResult loginResult = _PasswordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

            if (loginResult == PasswordVerificationResult.Success) {
            return user;
            // } else if (loginResult == PasswordVerificationResult.SuccessRehashNeeded) {
            } else{
                throw new Exception("Wrong username or password.");
            }

        }
    }
}