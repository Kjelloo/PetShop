using System;
using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Domain.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepo;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepo = ownerRepository;
        }

        public Owner New(string name)
        {
            var owner = new Owner {Name = name};
            return owner;
        }

        public Owner Save(Owner saveOwner)
        {
            if (_ownerRepo.GetAll().Contains(saveOwner))
                throw new Exception("Owner already exists in database");
            
            return _ownerRepo.Create(saveOwner);
        }

        public List<Owner> GetAll()
        {
            return _ownerRepo.GetAll().ToList();
        }

        public Owner GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID cannot be 0 or below...");
            
            return _ownerRepo.GetById(id);
        }

        public Owner Update(Owner updateOwner)
        {
            return _ownerRepo.Update(updateOwner);
        }

        public Owner Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("ID cannot be 0 or below...");

            return _ownerRepo.Delete(id);
        }
    }
}