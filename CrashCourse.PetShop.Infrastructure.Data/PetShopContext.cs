using System;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrashCourse.PetShop.Infrastructure.Data
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options) {}
        
        public DbSet<PetEntity> Pets { get; set; }
        public DbSet<PetTypeEntity> PetTypes { get; set; }
        public DbSet<OwnerEntity> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            for (var i = 1; i < 1000; i++)
            {
                modelBuilder.Entity<PetEntity>().HasData(new PetEntity()
                {
                    Id = i,
                    Name = "Name " + i
                });
            }            
            
            modelBuilder.Entity<PetTypeEntity>().HasData(new PetTypeEntity
            {
                Id = 1,
                Name = "cat"
            });
            
            modelBuilder.Entity<PetTypeEntity>().HasData(new PetTypeEntity
            {
                Id = 2,
                Name = "dog"
            });

            modelBuilder.Entity<OwnerEntity>().HasData(new OwnerEntity
            {
                Id = 1,
                Name = "Kjell"
            });
            
            modelBuilder.Entity<OwnerEntity>().HasData(new OwnerEntity
            {
                Id = 2,
                Name = "Mikkel"
            });
            
            /* 
            modelBuilder.Entity<PetEntity>().HasData(new PetEntity
            {
                Id = 1,
                Name = "Woof",
                BirthDate = DateTime.Now,
                SoldDate = DateTime.Now.AddHours(1),
                Color = "brown",
                OwnerId = 1,
                PetTypeId = 2,
                Price = 200
            });
            
            modelBuilder.Entity<PetEntity>().HasData(new PetEntity
            {
                Id = 2,
                Name = "Meow",
                BirthDate = DateTime.Now,
                SoldDate = DateTime.Now.AddHours(1),
                Color = "white",
                OwnerId = 2,
                PetTypeId = 1,
                Price = 150
            });*/
            
            
        }
    }
}