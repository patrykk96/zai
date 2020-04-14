using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            var result = await _userService.Register(userModel);

            if(result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginUserModel)
        {
            var result = await _userService.Login(loginUserModel);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}