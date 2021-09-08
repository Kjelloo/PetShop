using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IRepositories
{
    public interface IPetRepository
    {
        Pet Create(Pet petCreate);
        Pet Update(Pet petUpdate);
        IEnumerable<Pet> GetAllPets();
        Pet GetPetById(int id);
        Pet Delete(int id);
    }
}