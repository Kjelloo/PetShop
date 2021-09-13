using CrashCourse.PetShop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CrashCourse.PetShop.Infrastructure.Data
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options) {}
        
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        
        
    }
}