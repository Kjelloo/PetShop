using System;
using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Domain.Services
{
    public class PetTypeService : IPetTypeService
    {
        private readonly IPetTypeRepository _petTypeRepo;

        public PetTypeService(IPetTypeRepository petTypeRepository)
        {
            _petTypeRepo = petTypeRepository;
        }

        public PetType New(string name)
        {
            if (Exists(name)) return GetByName(name);
            var petType = new PetType {Name = name.ToLower()};
            return petType;
        }

        public PetType GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID cannot be 0 or below...");
            
            return _petTypeRepo.GetPetTypeByID(id);
        }

        public PetType GetByName(string name)
        {
            if (Exists(name))
            {
                return GetAll().FirstOrDefault(type => type.Name == name);
            }
            
            return null;
        }
        
        public List<PetType> GetAll()
        {
            return _petTypeRepo.GetAllPetTypes() != null ? _petTypeRepo.GetAllPetTypes().ToList() : null;
        }

        public bool Exists(string name)
        {
            return GetAll() != null && GetAll().Exists(petType => petType.Name == name.ToLower());
        }
        
        public PetType Update(PetType petTypeUpdate)
        {
            return _petTypeRepo.UpdatePetType(petTypeUpdate);
        }

        public PetType Save(PetType petTypeSave)
        {
            return _petTypeRepo.Create(petTypeSave);
        }

        public PetType GetMatching(PetType matchingType)
        {
            return GetAll().Exists(type => type.Name == matchingType.Name) ? GetByName(matchingType.Name) : null;
        }

        public PetType Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID cannot be 0 or below...");
            
            return _petTypeRepo.Delete(id);
        }
        
    }
}