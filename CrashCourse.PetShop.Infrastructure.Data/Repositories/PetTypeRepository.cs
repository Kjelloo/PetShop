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

        public PetType Create(PetType createPetType)
        {
            var entity = new PetTypeEntity
            {
                Name = createPetType.Name
            };
            
            var savedEntity = _ctx.PetTypes.Add(entity).Entity;
            _ctx.SaveChanges();
            return new PetType
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name
            };
        }

        public PetType GetById(int id)
        {
            return _ctx.PetTypes
                .Select(pt => new PetType
                {
                    Id = pt.Id,
                    Name = pt.Name
                })
                .FirstOrDefault(type => type.Id == id);
        }

        public IEnumerable<PetType> GetAll()
        {
            return _ctx.PetTypes
                .Select(p => new PetType
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
        }

        public PetType Update(PetType updatePetType)
        {
            var entity = new PetTypeEntity
            {
                Id = updatePetType.Id,
                Name = updatePetType.Name
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