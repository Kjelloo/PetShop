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

        public PetType NewPetType(string name)
        {
            if (Exists(name)) return GetPetTypeByName(name);
            var petType = new PetType {Name = name.ToLower()};
            return petType;
        }

        public PetType GetPetTypeById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID cannot be 0 or below...");
            
            return _petTypeRepo.GetPetTypeByID(id);
        }

        public PetType GetPetTypeByName(string name)
        {
            return !Exists(name) ? GetAllPetTypes().FirstOrDefault(type => type.Name == name) : null;
        }
        
        public List<PetType> GetAllPetTypes()
        {
            return _petTypeRepo.GetAllPetTypes() != null ? _petTypeRepo.GetAllPetTypes().ToList() : null;
        }

        public bool Exists(string name)
        {
            return GetAllPetTypes() != null && GetAllPetTypes().Exists(petType => petType.Name == name.ToLower());
        }
        
        public PetType UpdatePetType(PetType petTypeUpdate)
        {
            return _petTypeRepo.UpdatePetType(petTypeUpdate);
        }

        public PetType SavePetType(PetType petTypeSave)
        {
            if (GetAllPetTypes().Contains(petTypeSave))
                throw new Exception("Pet type is already saved to the database");
            
            return _petTypeRepo.Create(petTypeSave);
        }

        public PetType GetMatchingPetType(PetType matchingType)
        {
            return GetAllPetTypes().Exists(type => type.Name == matchingType.Name) ? GetPetTypeByName(matchingType.Name) : null;
        }

        public PetType DeletePetType(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID cannot be 0 or below...");
            
            return _petTypeRepo.Delete(id);
        }
        
    }
}