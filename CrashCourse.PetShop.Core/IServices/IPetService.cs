using System;
using System.Collections.Generic;
using CrashCourse.PetShop.Core.Filtering;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IServices
{
    public interface IPetService
    {
        Pet New(string name, PetType type, DateTime birthDate, DateTime soldDate, string color, double price);
        Pet Update(Pet updatePet);
        List<Pet> GetByColor(Filter filter, string color);
        List<Pet> SortByAscendingPrice(Filter filter);
        List<Pet> GetAll(Filter filter);
        List<Pet> GetFiveCheapest();
        Pet Delete(int id);
        Pet Save(Pet savePet);
        Pet GetById(int id);
        Pet CreateAndSave(string name, PetType petType, DateTime birthDate, DateTime soldDate, string color, double price);
        List<Pet> GetByType(PetType petType);
        int GetPetCount();
    }
}