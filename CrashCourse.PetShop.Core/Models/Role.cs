namespace CrashCourse.PetShop.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum RoleTypes
    {
        User,
        Administrator
    }
}