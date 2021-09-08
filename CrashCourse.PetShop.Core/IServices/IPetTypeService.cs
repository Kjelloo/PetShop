using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IServices
{
    public interface IPetTypeService
    {
        PetType NewPetType(string name);
        PetType GetPetTypeById(int id);
        PetType GetPetTypeByName(string name);
        List<PetType> GetAllPetTypes();
        bool Exists(string name);
        PetType UpdatePetType(PetType petTypeUpdate);
        PetType SavePetType(PetType petTypeSave);
        PetType GetMatchingPetType(PetType matchingType);
        PetType DeletePetType(int id);
    }
}