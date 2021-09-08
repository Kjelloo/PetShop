using System;
using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Domain.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepo;

        public PetService(IPetRepository petRepository)
        {
            _petRepo = petRepository;
        }

        //Creates a new pet
        public Pet NewPet(string name, PetType type, DateTime birthDate, DateTime soldDate, string color, double price)
        {
            var pet = new Pet
            {
                Name = name,
                Type = type,
                BirthDate = birthDate,
                SoldDate = soldDate,
                Color = color,
                Price = price
            };

            return pet;
        }

        public Pet UpdatePet(Pet petUpdate)
        {
            petUpdate = _petRepo.Update(petUpdate);
            return petUpdate;
        }

        public List<Pet> GetPetsByColor(string color)
        {
            // if not a number
            if (int.TryParse(color, out int n))
                throw new ArgumentException("Color has to be a string");
            
            return new List<Pet>(GetAllPets().Where(pet => pet.Color == color));
        }

        public List<Pet> SortPetsByAscendingPrice()
        {
            return new List<Pet>(GetAllPets().OrderBy(pet => pet.Price));
        }

        public List<Pet> GetAllPets()
        {
            return _petRepo.GetAllPets().ToList();
        }

        public List<Pet> GetFiveCheapestPets()
        {
            return SortPetsByAscendingPrice().Take(5).ToList();
        }

        public Pet DeletePet(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID cannot be 0 or below...");

            return _petRepo.Delete(id);
        }

        //Saves a pet
        public Pet SavePet(Pet petToSave)
        {
            if (GetAllPets().Contains(petToSave))
                throw new Exception("Pet is already saved to the database");
            
            return _petRepo.Create(petToSave);
        }

        public Pet GetPetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID cannot be 0 or below...");

            return _petRepo.GetPetById(id);
        }

        // Creates pet and saves it to the database
        public Pet CreateAndSavePet(string name, PetType petType, DateTime birthDate, DateTime soldDate, string color, double price)
        {
            var pet = NewPet(name, petType, birthDate, soldDate, color, price);
            
            if (GetAllPets().Contains(pet))
                throw new Exception("Pet is already saved to the database");
            
            return SavePet(pet);
        }

        public List<Pet> GetPetsByType(PetType petType)
        {
            return GetAllPets().FindAll(pet => Equals(pet.Type, petType));
        }
    }
}