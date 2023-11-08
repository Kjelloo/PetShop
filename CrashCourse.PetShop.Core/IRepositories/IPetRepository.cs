using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IRepositories
{
    public interface IPetRepository
    {
        Pet Create(Pet createPet);
        Pet Update(Pet updatePet);
        IEnumerable<Pet> GetAll();
        Pet GetById(int id);
        Pet Delete(int id);
        int Count();
    }
}