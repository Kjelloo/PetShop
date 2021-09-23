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
        public Pet New(string name, PetType type, DateTime birthDate, DateTime soldDate, string color, double price)
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

        public Pet Update(Pet updatePet)
        {
            return _petRepo.Update(updatePet);
        }

        public List<Pet> GetByColor(string color)
        {
            // if not a number
            if (int.TryParse(color, out int n))
                throw new ArgumentException("Color has to be a string");
            
            return new List<Pet>(GetAll().Where(pet => pet.Color == color));
        }

        public List<Pet> SortByAscendingPrice()
        {
            return new List<Pet>(GetAll().OrderBy(pet => pet.Price));
        }

        public List<Pet> GetAll()
        {
            return _petRepo.GetAll().ToList();
        }

        public List<Pet> GetFiveCheapest()
        {
            return SortByAscendingPrice().Take(5).ToList();
        }

        public Pet Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID cannot be 0 or below...");

            return _petRepo.Delete(id);
        }

        //Saves a pet
        public Pet Save(Pet savePet)
        {
            if (GetAll().Contains(savePet))
                throw new Exception("Pet is already saved to the database");
            
            return _petRepo.Create(savePet);
        }

        public Pet GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID cannot be 0 or below...");

            return _petRepo.GetById(id);
        }

        // Creates pet and saves it to the database
        public Pet CreateAndSave(string name, PetType petType, DateTime birthDate, DateTime soldDate, string color, double price)
        {
            var pet = New(name, petType, birthDate, soldDate, color, price);
            
            if (GetAll().Contains(pet))
                throw new Exception("Pet is already saved to the database");
            
            return Save(pet);
        }

        public List<Pet> GetByType(PetType petType)
        {
            return GetAll().FindAll(pet => Equals(pet.Type, petType));
        }
        
    }
}