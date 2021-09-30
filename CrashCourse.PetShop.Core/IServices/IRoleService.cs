using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IServices
{
    public interface IRoleService
    {
        Role New(int id, string name);
        List<Role> GetAll();
        Role GetById(int id);
        Role Update(Role updateRole);
        Role Delete(int id);
        Role Save(Role saveRole);
    }
}