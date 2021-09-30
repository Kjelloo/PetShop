using System.Collections.Generic;

namespace CrashCourse.PetShop.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Role> Roles { get; set; }
    }
}