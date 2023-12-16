using Microsoft.AspNetCore.Identity;
using WishListApi.Models;
using WishListApi.Models.DTOs;
using wish_list_service.Models.DTOs;
using System.Text.RegularExpressions;
using AutoMapper;

namespace WishListApi.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly PasswordHasher<User> _PasswordHasher = new PasswordHasher<User>();

        public LoginService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public bool IsUserExist(string login) {
            User? isUserExist = _dbContext.User.FirstOrDefault(u => u.Login.ToLower().Equals(login.ToLower()));
            return isUserExist != null;
        }

        public bool IsEmailExist(string email) {
            User? isEmailExist = _dbContext.User.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
            return isEmailExist != null;
        }

        public List<ErrorModel> ValidateRegisterFields(RegisterDto registerDto) {
            List<ErrorModel> errors = new List<ErrorModel>();

            Regex loginRegex = new Regex(@"^[A-z0-9](?:[A-z0-9]|[-_](?=[A-z0-9])){0,50}$");
            if (registerDto.Login.Length < 3) {
                errors.Add(new ErrorModel() {FieldName = "Login", Message = "Login must be longer than 3 characters.", Code = "LoginTooShort"});
            }
            if (registerDto.Login.Length > 50) {
                errors.Add(new ErrorModel() {FieldName = "Login", Message = "Login must be shorter than 50 characters.", Code = "LoginTooLong"});
            } else if (!loginRegex.IsMatch(registerDto.Login)) {
                errors.Add(new ErrorModel() {FieldName = "Login", Message = "The login has invalid characters. Allowed characters are letters (A-z), numbers (0-9) and the characters '-' and '_'.", Code = "LoginInvalid"});
            }

            Regex emailRegex = new Regex(@"^[a-z0-9]+(?:[-\._]?[a-z0-9]+)*@(?:[a-z0-9]+(?:-?[a-z0-9]+)*\.)+[a-z]+$");
            if (!emailRegex.IsMatch(registerDto.Email)) {
                errors.Add(new ErrorModel() {FieldName = "Email", Message = "Invalid email address format.", Code = "EmailInvalid"});
            }
            if (registerDto.Email.Length > 50) {
                errors.Add(new ErrorModel() {FieldName = "Email", Message = "Email must be shorter than 50 characters.", Code = "EmailTooLong"});
            }

            Regex nameRegex = new Regex(@"^[a-zA-Zà-ÿÀ-Ÿ\-]*$");
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

            Regex passwordRegex = new Regex(@"^((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,64})$");
            if (registerDto.Password.Length > 64) {
                errors.Add(new ErrorModel() {FieldName = "Password", Message = "Password must be shorter than 64 signs.", Code = "PasswordTooLong"});
            } else if (!passwordRegex.IsMatch(registerDto.Password)) {
                errors.Add(new ErrorModel() {FieldName = "Password", Message = "The password is too weak. A strong password must consist of a minimum of 8 characters, including one lowercase letter (a-z), one uppercase letter (A-Z), one number (0-9) and one character (e.g. ';', '@', '#').", Code = "PasswordInvalid"});
            }

            return errors;
        }

        public UserVm Register(RegisterDto registerDto)
        {
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
            User? addedUser = _dbContext.User.Find(registerDto.Login);
           
           if (addedUser != null) {
            return _mapper.Map<UserVm>(addedUser);
           }
           else {
            throw new Exception("Object cannot be get.");
           }
        }

        public bool IsUserActive(string login) {
            User? user = _dbContext.User.Find(login);
            if (user != null) {
                return user.IsActive;
            }
            return false;
        }

        public UserVm Login(LoginDto loginDto)
        {
            var user = _dbContext.User.Find(loginDto.Login);

            if (user == null) {
                return null;
            }

            PasswordVerificationResult loginResult = _PasswordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

            if (loginResult == PasswordVerificationResult.Success) {
                return _mapper.Map<UserVm>(user);

            // } else if (loginResult == PasswordVerificationResult.SuccessRehashNeeded) {
            } else{
                return null;
            }

        }
    }
}