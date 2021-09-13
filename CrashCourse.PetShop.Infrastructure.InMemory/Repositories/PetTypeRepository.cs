using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Infrastructure.InMemory.Repositories
{
    public class PetTypeRepository : IPetTypeRepository
    {
        private readonly List<PetType> _petTypes;
        private int _id;

        public PetTypeRepository(FakeDb fakeDb)
        {
            _petTypes = fakeDb.PetTypes;
            _id = fakeDb.PetTypeId;
        }

        public PetType Create(PetType petTypeCreate)
        {
            petTypeCreate.Id = _id++;
            _petTypes.Add(petTypeCreate);
            return petTypeCreate;
        }

        public PetType GetPetTypeByID(int id)
        {
            return _petTypes.FirstOrDefault(petType => petType.Id == id);
        }

        public List<PetType> GetAllPetTypes()
        {
            return _petTypes;
        }

        public PetType UpdatePetType(PetType petTypeUpdate)
        {
            var petTypeDb = GetPetTypeByID(petTypeUpdate.Id);
            petTypeDb.Name = petTypeUpdate.Name.ToLower();
            return petTypeDb;
        }

        public PetType Delete(int id)
        {
            var petTypeDb = GetPetTypeByID(id);
            if (petTypeDb == null) return null;
            _petTypes.Remove(petTypeDb);
            return petTypeDb;
        }
    }
}