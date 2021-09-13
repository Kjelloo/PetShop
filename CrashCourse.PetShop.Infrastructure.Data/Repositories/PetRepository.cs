using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.Models;

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
            var pet = _ctx.Pets.Add(petCreate).Entity;
            _ctx.SaveChanges();
            return pet;
        }

        public Pet Update(Pet petUpdate)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _ctx.Pets;
        }

        public Pet GetPetById(int id)
        {
            return _ctx.Pets.FirstOrDefault(pet => pet.Id == id);
        }

        public Pet Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}