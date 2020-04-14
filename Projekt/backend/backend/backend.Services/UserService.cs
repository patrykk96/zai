using backend.Data.DbModels;
using backend.Data.Dto;
using backend.Data.Enums;
using backend.Data.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // TODO: Expand user registration, add email confirmation
        public async Task<ResultDto<BaseDto>> Register(UserModel userModel)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            // TODO: implement mapper
            var user = new User()
            {
                Email = userModel.Email,
                UserName = userModel.Username,
            };

            try
            {
                var createUserResult = await _userManager.CreateAsync(user, userModel.Password);

                if (createUserResult.Succeeded)
                {
                    var addUserRoleResult = await _userManager.AddToRoleAsync(user, UserRole.User.ToString());

                    if (addUserRoleResult.Succeeded)
                    {
                        return result;
                    }

                    result.Error = addUserRoleResult.Errors.First().Description;

                    return result;
                }

                result.Error = createUserResult.Errors.First().Description;

                return result;
            }
            catch (Exception e)
            {
                result.Error = e.Message;

                return result;
            }

        }

        public async Task<ResultDto<LoginDto>> Login(LoginModel loginModel)
        {
            var result = new ResultDto<LoginDto>()
            {
                Error = null
            };

            //TODO: add option to login with email
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user == null)
            {
                result.Error = "Niepoprawne dane";
                return result;
            }

            //TODO: Implement "remember me" option
            var loginAttempt = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, false, false);

            if (loginAttempt.Succeeded)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                var role = userRole[0];
                var token = GenerateJwtToken(user, role);
                var tokenDto = new LoginDto()
                {
                    Token = token
                };

                result.SuccessResult = tokenDto;

                return result;
            }

            //TODO: check if user activated account

            result.Error = "Nie udało się zalogować";

            return result;
        }

        private object GenerateJwtToken(User user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Auth:Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Auth:Jwt:Issuer"],
                _configuration["Auth:Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
