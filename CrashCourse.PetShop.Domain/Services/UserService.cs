using System;
using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User New(string username, string password, byte[] passwordHash, byte[] passwordSalt, List<Role> roles)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Roles = roles
            };

            return user;
        }

        public User GetById(int id)
        {
            if (id == 0)
                throw new ArgumentException("Id cannot be 0");
            
            return _userRepository.GetById(id);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public User Save(User user)
        {
            return _userRepository.Create(user);
        }
    }
}