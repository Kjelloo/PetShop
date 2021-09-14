using System;
using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IServices
{
    public interface IPetService
    {
        Pet New(string name, PetType type, DateTime birthDate, DateTime soldDate, string color, double price);
        Pet Update(Pet petUpdate);
        List<Pet> GetByColor(string color);
        List<Pet> SortByAscendingPrice();
        List<Pet> GetAll();
        List<Pet> GetFiveCheapest();
        Pet Delete(int id);
        Pet Save(Pet petToSave);
        Pet GetById(int id);
        Pet CreateAndSave(string name, PetType petType, DateTime birthDate, DateTime soldDate, string color, double price);
        List<Pet> GeByType(PetType petType);
    }
}