using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IServices
{
    public interface IOwnerService
    {
        Owner New(string name);
        Owner Save(Owner saveOwner);
        List<Owner> GetAll();
        Owner GetById(int id);
        Owner Update(Owner updateOwner);
        Owner Delete(int id);
    }
}