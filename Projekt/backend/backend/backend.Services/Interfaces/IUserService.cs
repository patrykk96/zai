using backend.Data.Dto;
using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResultDto<BaseDto>> Register(UserModel userModel);
        Task<ResultDto<LoginDto>> Login(LoginModel loginModel);
    }
}
