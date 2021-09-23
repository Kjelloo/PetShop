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

        public PetType Create(PetType createPetType)
        {
            createPetType.Id = _id++;
            _petTypes.Add(createPetType);
            return createPetType;
        }

        public PetType GetById(int id)
        {
            return _petTypes.FirstOrDefault(petType => petType.Id == id);
        }

        public IEnumerable<PetType> GetAll()
        {
            return _petTypes;
        }

        public PetType Update(PetType updatePetType)
        {
            var petTypeDb = GetById(updatePetType.Id);
            petTypeDb.Name = updatePetType.Name.ToLower();
            return petTypeDb;
        }

        public PetType Delete(int id)
        {
            var petTypeDb = GetById(id);
            if (petTypeDb == null) return null;
            _petTypes.Remove(petTypeDb);
            return petTypeDb;
        }
    }
}