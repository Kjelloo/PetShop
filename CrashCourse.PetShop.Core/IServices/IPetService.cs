using System;
using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IServices
{
    public interface IPetService
    {
        Pet NewPet(string name, PetType type, DateTime birthDate, DateTime soldDate, string color, double price);
        Pet UpdatePet(Pet petUpdate);
        List<Pet> GetPetsByColor(string color);
        List<Pet> SortPetsByAscendingPrice();
        List<Pet> GetAllPets();
        List<Pet> GetFiveCheapestPets();
        Pet DeletePet(int id);
        Pet SavePet(Pet petToSave);
        Pet GetPetById(int id);
        Pet CreateAndSavePet(string name, PetType petType, DateTime birthDate, DateTime soldDate, string color, double price);
    }
}