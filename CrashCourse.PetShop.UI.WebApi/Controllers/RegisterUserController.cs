using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrashCourse.PetShop.Auth.Helpers;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.UI.WebApi.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrashCourse.PetShop.UI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationHelper _helper;

        public RegisterUserController(IUserService userService, IAuthenticationHelper helper)
        {
            _userService = userService;
            _helper = helper;
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] PostUserDto userDto)
        {
            _helper.CreatePasswordHash(userDto.Password, out var passwordHash, out var salt);

            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                IsAdmin = false,
                PasswordHash = passwordHash,
                PasswordSalt = salt
            };

            _userService.Save(user);

            user = _userService.GetAll().FirstOrDefault(u => u.Username == userDto.Username);

            return Ok(new
            {
                username = user.Username,
                token = _helper.GenerateToken(user)
            });
        }
    }
}