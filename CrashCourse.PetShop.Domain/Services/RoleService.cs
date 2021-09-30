using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Domain.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role New(int id, string name)
        {
            return new Role
            {
                Id = id,
                Name = name
            };
        }

        public List<Role> GetAll()
        {
            return _roleRepository.GetAll().ToList();
        }

        public Role GetById(int id)
        {
            return _roleRepository.GetById(id);
        }

        public Role Update(Role updateRole)
        {
            return _roleRepository.Update(updateRole);
        }

        public Role Delete(int id)
        {
            return _roleRepository.Delete(id);
        }

        public Role Save(Role saveRole)
        {
            return _roleRepository.Create(saveRole);
        }
    }
}