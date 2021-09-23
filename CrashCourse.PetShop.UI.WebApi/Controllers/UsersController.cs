using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace CrashCourse.PetShop.UI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<User> GetAll()
        {
            var users = _userService.GetAll();
            
            if (users.Count == 0)
                return NoContent();
            
            return Ok(users);
        }
    }
}