﻿using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Core.IServices
{
    public interface IUserService
    {
        User New(string username, string password, byte[] passwordHash, byte[] passwordSalt, List<Role> roles);
        User GetById(int id);
        List<User> GetAll();
        User Save(User user);
    }
}