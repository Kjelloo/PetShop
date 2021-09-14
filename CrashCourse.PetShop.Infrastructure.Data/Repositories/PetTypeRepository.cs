using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.Infrastructure.Data.Entities;

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
            var entity = new PetTypeEntity
            {
                Name = petTypeCreate.Name
            };
            
            var savedEntity = _ctx.PetTypes.Add(entity).Entity;
            _ctx.SaveChanges();
            return new PetType
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name
            };
        }

        public PetType GetPetTypeByID(int id)
        {
            return _ctx.PetTypes
                .Select(pt => new PetType
                {
                    Id = pt.Id,
                    Name = pt.Name
                })
                .FirstOrDefault(type => type.Id == id);
        }

        public IEnumerable<PetType> GetAllPetTypes()
        {
            return _ctx.PetTypes
                .Select(p => new PetType
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
        }

        public PetType UpdatePetType(PetType petTypeUpdate)
        {
            var entity = new PetTypeEntity
            {
                Id = petTypeUpdate.Id,
                Name = petTypeUpdate.Name
            };
            
            var savedEntity = _ctx.PetTypes.Update(entity).Entity;
            _ctx.SaveChanges();
            return new PetType
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name
            };
        }

        public PetType Delete(int id)
        {
            var petTypeDeleted = _ctx.PetTypes.Remove(new PetTypeEntity() {Id = id});
            _ctx.SaveChanges();

            return new PetType
            {
                Id = petTypeDeleted.Entity.Id,
                Name = petTypeDeleted.Entity.Name
            };
            
        }
    }
}