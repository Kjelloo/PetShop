using CrashCourse.PetShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrashCourse.PetShop.Infrastructure.Data
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options) {}
        
        public DbSet<PetEntity> Pets { get; set; }
        public DbSet<PetTypeEntity> PetTypes { get; set; }

    }
}