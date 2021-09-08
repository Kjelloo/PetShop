using System;
using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Infrastructure.InMemory.Repositories
{
    public class PetRepository : IPetRepository
    {
        private static int _id;
        private readonly List<Pet> _pets;

        public PetRepository(FakeDb fakeDb)
        {
            _pets = fakeDb.Pets;
            _id = fakeDb.PetId;
        }

        public Pet Create(Pet petCreate)
        {
            petCreate.Id = _id++;
            _pets.Add(petCreate);
            return petCreate;
        }

        public Pet Update(Pet petUpdate)
        {
            var petDb = GetPetById(petUpdate.Id);
            
            if (petDb == null) return null;

            if (petUpdate.Name != null)
                petDb.Name = petUpdate.Name;
            
            if (petUpdate.Color != null)
                petDb.Color = petUpdate.Color;
            
            if (petUpdate.Price != null)
                petDb.Price = petUpdate.Price;
            
            if (petUpdate.Type != null)
                petDb.Type = petUpdate.Type;
            
            if (petUpdate.BirthDate != DateTime.MinValue)
                petDb.BirthDate = petUpdate.BirthDate;
            
            if (petUpdate.SoldDate != DateTime.MinValue) 
                petDb.SoldDate = petUpdate.SoldDate;

            return petDb;
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _pets;
        }

        public Pet GetPetById(int id)
        {
            /*return _pets
                .Select(p => new Pet
                {
                    Id = p.Id,
                    BirthDate = p.BirthDate,
                    Color = p.Color,
                    Name = p.Name,
                    Price = p.Price,
                    SoldDate = p.SoldDate,
                    Type = p.Type
                })
                .FirstOrDefault(pet => pet.Id == id);*/

            return _pets.FirstOrDefault(pet => pet.Id == id);
        }

        public Pet Delete(int id)
        {
            var petDb = GetPetById(id);
            if (petDb == null) return null;
            _pets.Remove(petDb);
            return petDb;
        }
    }
}