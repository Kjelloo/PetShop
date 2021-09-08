using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IRepositories
{
    public interface IPetTypeRepository
    {
        PetType Create(PetType petTypeCreate);
        PetType GetPetTypeByID(int id);
        List<PetType> GetAllPetTypes();
        PetType UpdatePetType(PetType petTypeUpdate);
        PetType Delete(int id);
    }
}