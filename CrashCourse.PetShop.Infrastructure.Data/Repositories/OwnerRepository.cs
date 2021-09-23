using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.Infrastructure.Data.Entities;

namespace CrashCourse.PetShop.Infrastructure.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly PetShopContext _ctx;

        public OwnerRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public Owner Create(Owner createOwner)
        {
            var entity = new OwnerEntity
            {
                Name = createOwner.Name
            };

            var savedEntity = _ctx.Owners.Add(entity).Entity;
            _ctx.SaveChanges();

            return new Owner
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name
            };
        }

        public IEnumerable<Owner> GetAll()
        {
            return _ctx.Owners
                .Select(ow => new Owner
                {
                    Id = ow.Id,
                    Name = ow.Name
                }).ToList();
        }

        public Owner GetById(int id)
        {
            return _ctx.Owners
                .Select(ow => new Owner
                {
                    Id = ow.Id,
                    Name = ow.Name
                }).FirstOrDefault(ow => ow.Id == id);
        }

        public Owner Update(Owner ownerUpdate)
        {
            var entity = new OwnerEntity
            {
                Id = ownerUpdate.Id,
                Name = ownerUpdate.Name
            };

            var savedEntity = _ctx.Owners.Update(entity).Entity;
            _ctx.SaveChanges();

            return new Owner
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name
            };
        }

        public Owner Delete(int id)
        {
            var ownerDeleted = _ctx.Owners.Remove(new OwnerEntity() {Id = id}).Entity;
            _ctx.SaveChanges();

            return new Owner
            {
                Id = ownerDeleted.Id,
                Name = ownerDeleted.Name
            };
        }
    }
}