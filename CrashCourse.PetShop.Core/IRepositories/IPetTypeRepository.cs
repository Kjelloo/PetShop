using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IRepositories
{
    public interface IPetTypeRepository
    {
        PetType Create(PetType createPetType);
        PetType GetById(int id);
        IEnumerable<PetType> GetAll();
        PetType Update(PetType updatePetType);
        PetType Delete(int id);
    }
}
