using Microsoft.AspNetCore.Identity;
using WishListApi.Models;
using WishListApi.Models.DTOs;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WishListApi.Context;

namespace WishListApi.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly PasswordHasher<User> _PasswordHasher = new PasswordHasher<User>();

        public LoginService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        private string GenerateToken(User user) {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.ConfigurationManager.AppSetting["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: Configuration.ConfigurationManager.AppSetting["JWT:ValidIssuer"], 
                audience: Configuration.ConfigurationManager.AppSetting["JWT:ValidAudience"], 
                claims: new List<Claim>() {
                    new(JwtRegisteredClaimNames.Sub, user.Login),
                    new("firstName", user.FirstName),
                    new("lastName", user.LastName),
                }, 
                expires: DateTime.Now.AddMinutes(10), 
                signingCredentials: signingCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public bool IsUserExist(string login) {
            User? isUserExist = _dbContext.Users.FirstOrDefault(u => u.Login.ToLower().Equals(login.ToLower()));
            return isUserExist != null;
        }

        public bool IsEmailExist(string email) {
            User? isEmailExist = _dbContext.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
            return isEmailExist != null;
        }

        public List<ErrorModel> ValidateRegisterFields(RegisterDto registerDto) {
            List<ErrorModel> errors = new List<ErrorModel>();

            Regex loginRegex = new(@"^[A-z0-9](?:[A-z0-9]|[-_](?=[A-z0-9])){0,50}$");
            if (registerDto.Login.Length < 3) {
                errors.Add(new ErrorModel() {FieldName = "Login", Message = "Login must be longer than 3 characters.", Code = "LoginTooShort"});
            }
            if (registerDto.Login.Length > 50) {
                errors.Add(new ErrorModel() {FieldName = "Login", Message = "Login must be shorter than 50 characters.", Code = "LoginTooLong"});
            } else if (!loginRegex.IsMatch(registerDto.Login)) {
                errors.Add(new ErrorModel() {FieldName = "Login", Message = "The login has invalid characters. Allowed characters are letters (A-z), numbers (0-9) and the characters '-' and '_'.", Code = "LoginInvalid"});
            }

            Regex emailRegex = new(@"^[a-z0-9]+(?:[-\._]?[a-z0-9]+)*@(?:[a-z0-9]+(?:-?[a-z0-9]+)*\.)+[a-z]+$");
            if (!emailRegex.IsMatch(registerDto.Email)) {
                errors.Add(new ErrorModel() {FieldName = "Email", Message = "Invalid email address format.", Code = "EmailInvalid"});
            }
            if (registerDto.Email.Length > 50) {
                errors.Add(new ErrorModel() {FieldName = "Email", Message = "Email must be shorter than 50 characters.", Code = "EmailTooLong"});
            }

            Regex nameRegex = new(@"^[a-zA-Zà-ÿÀ-Ÿ\-]*$");
            if (!nameRegex.IsMatch(registerDto.FirstName)) {
                errors.Add(new ErrorModel() {FieldName = "FirstName", Message = "Invalid first name format.", Code = "FirstNameInvalid"});
            }
            if (registerDto.FirstName.Length < 2) {
                errors.Add(new ErrorModel() {FieldName = "FirstName", Message = "First name must be longer than 2 characters.", Code = "FirstNameTooShort"});
            }
            if (registerDto.FirstName.Length > 50) {
                errors.Add(new ErrorModel() {FieldName = "FirstName", Message = "First name must be shorter than 50 characters.", Code = "FirstNameTooLong"});
            }
            if (!nameRegex.IsMatch(registerDto.LastName)) {
                errors.Add(new ErrorModel() {FieldName = "LastName", Message = "Invalid last name format.", Code = "LastNameInvalid"});
            }
            if (registerDto.LastName.Length < 2) {
                errors.Add(new ErrorModel() {FieldName = "LastName", Message = "Last name must be longer than 2 characters.", Code = "LastNameTooShort"});
            }
            if (registerDto.LastName.Length > 50) {
                errors.Add(new ErrorModel() {FieldName = "LastName", Message = "Last name must be shorter than 50 characters.", Code = "LastNameTooLong"});
            }

            Regex passwordRegex = new(@"^((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,64})$");
            if (registerDto.Password.Length > 64) {
                errors.Add(new ErrorModel() {FieldName = "Password", Message = "Password must be shorter than 64 signs.", Code = "PasswordTooLong"});
            } else if (!passwordRegex.IsMatch(registerDto.Password)) {
                errors.Add(new ErrorModel() {FieldName = "Password", Message = "The password is too weak. A strong password must consist of a minimum of 8 characters, including one lowercase letter (a-z), one uppercase letter (A-Z), one number (0-9) and one character (e.g. ';', '@', '#').", Code = "PasswordInvalid"});
            }

            return errors;
        }

        public string Register(RegisterDto registerDto)
        {
            User newUser = new()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Login = registerDto.Login,
                IsActive = false,
                RegisterDate = DateTime.UtcNow
            };
            newUser.Password = _PasswordHasher.HashPassword(newUser,registerDto.Password);

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            _dbContext.Users.Find(registerDto.Login);
           
            return GenerateToken(newUser);
        }

        public bool IsUserActive(string login) {
            User? user = _dbContext.Users.FirstOrDefault(u => u.Login == login);
            if (user != null) {
                return user.IsActive;
            }
            return false;
        }

        public string Login(LoginDto loginDto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Login == loginDto.Login);

            if (user == null) {
                return null;
            }

            PasswordVerificationResult loginResult = _PasswordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

            if (loginResult == PasswordVerificationResult.Success) {
                return GenerateToken(user);
            // } else if (loginResult == PasswordVerificationResult.SuccessRehashNeeded) {
            } else{
                return null;
            }

        }
    }
}