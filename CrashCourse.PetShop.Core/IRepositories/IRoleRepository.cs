using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IRepositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        Role Create(Role roleCreate);
        Role Update(Role roleUpdate);
        Role Delete(int id);
    }
}