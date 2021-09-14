using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IServices
{
    public interface IPetTypeService
    {
        PetType New(string name);
        PetType GetById(int id);
        PetType GetByName(string name);
        List<PetType> GetAll();
        bool Exists(string name);
        PetType Update(PetType petTypeUpdate);
        PetType Save(PetType petTypeSave);
        PetType GetMatching(PetType matchingType);
        PetType Delete(int id);
    }
}