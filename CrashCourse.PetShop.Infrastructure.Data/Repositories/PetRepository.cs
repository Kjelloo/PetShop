using System;
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

        public Pet Create(Pet createPet)
        {

            var entity = new PetEntity
            {
                Name = createPet.Name,
                BirthDate = createPet.BirthDate,
                SoldDate = createPet.SoldDate,
                Color = createPet.Color,
                Price = createPet.Price,
                PetTypeId = createPet.Type.Id
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

        public Pet Update(Pet updatePet)
        {
            var entity = new PetEntity
            {
                Id = updatePet.Id,
                Name = updatePet.Name,
                BirthDate = updatePet.BirthDate,
                SoldDate = updatePet.SoldDate,
                Color = updatePet.Color,
                Price = updatePet.Price,
                PetTypeId = updatePet.Type.Id
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

        public IEnumerable<Pet> GetAll()
        {
            var selectQuery = _ctx.Pets
                .Select(pe => new Pet
                {
                    Id = pe.Id,
                    BirthDate = pe.BirthDate,
                    Color = pe.Color,
                    Name = pe.Name,
                    Price = pe.Price,
                    SoldDate = pe.SoldDate,
                    Type = new PetType {Id = pe.PetTypeId}
                });
            
            return selectQuery.ToList();
        }

        public Pet GetById(int id)
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
            var petDeleted = _ctx.Pets.Remove(new PetEntity() {Id = id}).Entity;
            _ctx.SaveChanges();
            
            return new Pet
            {
                Id = petDeleted.Id,
                Name = petDeleted.Name,
                BirthDate = petDeleted.BirthDate,
                Color = petDeleted.Color,
                Price = petDeleted.Price,
                SoldDate = petDeleted.SoldDate,
                Type = new PetType {Id = petDeleted.PetTypeId}
            };
        }

        public int Count()
        {
            return _ctx.Pets.Count();
        }
    }
}