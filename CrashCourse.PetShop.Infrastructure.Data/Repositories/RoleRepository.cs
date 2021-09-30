using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.Infrastructure.Data.Entities;

namespace CrashCourse.PetShop.Infrastructure.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PetShopContext _ctx;

        public RoleRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Role> GetAll()
        {
            return _ctx.Roles.Select(
                ue => new Role
                {
                    Id = ue.Id,
                    Name = ue.Name
                }).ToList();
        }

        public Role GetById(int id)
        {
            return _ctx.Roles.Select(
                ue => new Role
                {
                    Id = ue.Id,
                    Name = ue.Name
                }).FirstOrDefault(ue => ue.Id == id);
        }

        public Role Create(Role roleCreate)
        {
            var entity = new RoleEntity
            {
                Name = roleCreate.Name
            };

            var savedEntity = _ctx.Roles.Add(entity).Entity;
            _ctx.SaveChanges();

            return new Role
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name
            };
        }

        public Role Update(Role roleUpdate)
        {
            throw new System.NotImplementedException();
        }

        public Role Delete(int id)
        {
            var roleDeleted = _ctx.Roles.Remove(new RoleEntity {Id = id}).Entity;
            _ctx.SaveChanges();
            
            return new Role
            {
                Id = roleDeleted.Id,
                Name = roleDeleted.Name,
            };
        }
    }
}