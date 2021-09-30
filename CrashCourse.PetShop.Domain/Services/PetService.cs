using System;
using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.Filtering;
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

        public List<Pet> GetByColor(Filter filter, string color)
        {
            // if not a number
            if (int.TryParse(color, out int n))
                throw new ArgumentException("Color has to be a string");
            
            return new List<Pet>(GetAll(filter).Where(pet => pet.Color == color));
        }

        public List<Pet> SortByAscendingPrice(Filter filter)
        {
            return new List<Pet>(GetAll(filter).OrderBy(pet => pet.Price));
        }

        public List<Pet> GetAll(Filter filter)
        {

            if (filter == null)
            {
                return _petRepo.GetAll(null).ToList();
            }

            var totalCount = _petRepo.Count();
            
            if (filter.Page < 1 || (filter.Page - 1) * filter.Count > totalCount)
            {
                throw new ArgumentException("Page exceeds total pet count, max page allowed with current count: " +
                                  (totalCount / filter.Count + 1));
            }

            if (GetAll(null).Count == 0)
                throw new Exception("No pets found");
            
            return _petRepo.GetAll(filter).ToList();
        }

        public List<Pet> GetFiveCheapest()
        {
            return SortByAscendingPrice(null).Take(5).ToList();
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
            if (GetAll(null).Contains(savePet))
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
            
            if (GetAll(null).Contains(pet))
                throw new Exception("Pet is already saved to the database");
            
            return Save(pet);
        }

        public List<Pet> GetByType(PetType petType)
        {
            return GetAll(null).FindAll(pet => Equals(pet.Type, petType));
        }

        public int GetPetCount()
        {
            return _petRepo.Count();
        }
    }
}