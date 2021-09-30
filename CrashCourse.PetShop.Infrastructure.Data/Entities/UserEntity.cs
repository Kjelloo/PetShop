using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Infrastructure.Data.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Role> Roles { get; set; }
    }
}