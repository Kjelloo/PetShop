using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.Infrastructure.Data.Entities;

namespace CrashCourse.PetShop.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PetShopContext _ctx;

        public UserRepository(PetShopContext dbContext)
        {
            _ctx = dbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _ctx.Users.Select(
                ue => new User
                {
                    Id = ue.Id,
                    Username = ue.Username,
                    Password = ue.Password,
                    PasswordHash = ue.PasswordHash,
                    PasswordSalt = ue.PasswordSalt,
                    IsAdmin = ue.IsAdmin
                }).ToList();
        }

        public User GetById(int id)
        {
            return _ctx.Users.Select(
                ue => new User
                {
                    Id = ue.Id,
                    Username = ue.Username,
                    Password = ue.Password,
                    PasswordHash = ue.PasswordHash,
                    PasswordSalt = ue.PasswordSalt,
                    IsAdmin = ue.IsAdmin
                }).FirstOrDefault(ue => ue.Id == id);
        }

        public User Create(User user)
        {
            var entity = new UserEntity
            {
                Username = user.Username,
                Password = user.Password,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                IsAdmin = user.IsAdmin
            };

            var savedEntity = _ctx.Users.Add(entity).Entity;
            _ctx.SaveChanges();

            return new User
            {
                Id = savedEntity.Id,
                Username = savedEntity.Username,
                Password = savedEntity.Password,
                PasswordHash = savedEntity.PasswordHash,
                PasswordSalt = savedEntity.PasswordSalt,
                IsAdmin = savedEntity.IsAdmin
            };
        }
    }
}