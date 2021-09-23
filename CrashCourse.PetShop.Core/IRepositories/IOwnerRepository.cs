using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IRepositories
{
    public interface IOwnerRepository
    {
        Owner Create(Owner createOwner);
        IEnumerable<Owner> GetAll();
        Owner GetById(int id);
        Owner Update(Owner ownerUpdate);
        Owner Delete(int id);
    }
}