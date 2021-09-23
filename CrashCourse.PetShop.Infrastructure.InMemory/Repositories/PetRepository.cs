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

        public Pet Create(Pet createPet)
        {
            createPet.Id = _id++;
            _pets.Add(createPet);
            return createPet;
        }

        public Pet Update(Pet updatePet)
        {
            var petDb = GetById(updatePet.Id);
            
            if (petDb == null) return null;

            if (updatePet.Name != null)
                petDb.Name = updatePet.Name;
            
            if (updatePet.Color != null)
                petDb.Color = updatePet.Color;
            
            if (updatePet.Price != null)
                petDb.Price = updatePet.Price;
            
            if (updatePet.Type != null)
                petDb.Type = updatePet.Type;
            
            if (updatePet.BirthDate != DateTime.MinValue)
                petDb.BirthDate = updatePet.BirthDate;
            
            if (updatePet.SoldDate != DateTime.MinValue) 
                petDb.SoldDate = updatePet.SoldDate;

            return petDb;
        }

        public IEnumerable<Pet> GetAll()
        {
            return _pets;
        }

        public Pet GetById(int id)
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
            var petDb = GetById(id);
            if (petDb == null) return null;
            _pets.Remove(petDb);
            return petDb;
        }
    }
}