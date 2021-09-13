using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Infrastructure.Data.Repositories
{
    public class PetTypeRepository : IPetTypeRepository
    {
        private readonly PetShopContext _ctx;

        public PetTypeRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public PetType Create(PetType petTypeCreate)
        {
            var petType = _ctx.PetTypes.Add(petTypeCreate).Entity;
            _ctx.SaveChanges();
            return petType;
        }

        public PetType GetPetTypeByID(int id)
        {
            return _ctx.PetTypes.FirstOrDefault(type => type.Id == id);
        }

        public IEnumerable<PetType> GetAllPetTypes()
        {
            return _ctx.PetTypes;
        }

        public PetType UpdatePetType(PetType petTypeUpdate)
        {
            throw new System.NotImplementedException();
        }

        public PetType Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}