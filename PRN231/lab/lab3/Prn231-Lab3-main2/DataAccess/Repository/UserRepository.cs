using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObject.Models;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext db;

        public UserRepository(MyDBContext myDb)
        {
            db = myDb;
        }
        public Task<User> GetByIdAsync(object id)
        {
            try
            {
                var user = db.Users.FirstOrDefaultAsync(c => c.Id == (int)id).Result;
                
                return Task.FromResult(user);
            }
            catch
            {
                return null;
            }
        }

        public Task<User> GetByUsernameAsync(string username)
        {
            try
            {
                var user = db.Users.FirstOrDefaultAsync(c => c.Username == username).Result;

                return Task.FromResult(user);
            }
            catch
            {
                return null;
            }
        }

        public Task<User> SaveUser(User user)
        {
            try
            {
                User tmp = db.Users.FirstOrDefault(c => c.Username == user.Username);
                if (tmp != null)
                {
                    return null;
                }

                db.Users.Add(user);
                db.SaveChanges();
                return Task.FromResult(user);
            }
            catch
            {
                return null;
            }
        }
    }
}
