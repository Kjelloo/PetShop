using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.Infrastructure.Data.Entities;

namespace CrashCourse.PetShop.Infrastructure.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetShopContext _ctx;

        public PetRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public Pet Create(Pet petCreate)
        {

            var entity = new PetEntity
            {
                Name = petCreate.Name,
                BirthDate = petCreate.BirthDate,
                SoldDate = petCreate.SoldDate,
                Color = petCreate.Color,
                Price = petCreate.Price,
                PetTypeId = petCreate.Type.Id
            };
            
            var savedEntity = _ctx.Pets.Add(entity).Entity;
            _ctx.SaveChanges();
            return new Pet
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name,
                BirthDate = savedEntity.BirthDate,
                Color = savedEntity.Color,
                Price = savedEntity.Price,
                SoldDate = savedEntity.SoldDate,
                Type = new PetType {Id = savedEntity.PetTypeId}
            };
        }

        public Pet Update(Pet petUpdate)
        {
            var entity = new PetEntity()
            {
                Id = petUpdate.Id,
                Name = petUpdate.Name,
                BirthDate = petUpdate.BirthDate,
                SoldDate = petUpdate.SoldDate,
                Color = petUpdate.Color,
                Price = petUpdate.Price,
                PetTypeId = petUpdate.Type.Id
            };
            
            var savedEntity = _ctx.Pets.Update(entity).Entity;
            _ctx.SaveChanges();
            return new Pet
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name,
                BirthDate = savedEntity.BirthDate,
                Color = savedEntity.Color,
                Price = savedEntity.Price,
                SoldDate = savedEntity.SoldDate,
                Type = new PetType {Id = savedEntity.PetTypeId}
            };
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _ctx.Pets
                .Select(pe => new Pet
                {
                    Id = pe.Id,
                    BirthDate = pe.BirthDate,
                    Color = pe.Color,
                    Name = pe.Name,
                    Price = pe.Price,
                    SoldDate = pe.SoldDate,
                    Type = new PetType {Id = pe.PetTypeId}
                })
                .ToList();
        }

        public Pet GetPetById(int id)
        {
            return _ctx.Pets
                .Select(pe => new Pet 
                {
                    Id = pe.Id,
                    BirthDate = pe.BirthDate,
                    Color = pe.Color,
                    Name = pe.Name,
                    Price = pe.Price,
                    SoldDate = pe.SoldDate,
                    Type = new PetType {Id = pe.PetTypeId}})
                .FirstOrDefault(pet => pet.Id == id);
        }

        public Pet Delete(int id)
        {
            var petDeleted = _ctx.Pets.Remove(new PetEntity() {Id = id});
            _ctx.SaveChanges();
            
            return new Pet
            {
                Id = petDeleted.Entity.Id,
                Name = petDeleted.Entity.Name,
                BirthDate = petDeleted.Entity.BirthDate,
                Color = petDeleted.Entity.Color,
                Price = petDeleted.Entity.Price,
                SoldDate = petDeleted.Entity.SoldDate,
                Type = new PetType {Id = petDeleted.Entity.PetTypeId}
            };
        }
    }
}