using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.Interface
{
    public interface IUserService
    {
        public Task<User> GetByIdAsync(object id);
        public Task<User> GetByUsernameAsync(string username);
        public Task<User> SaveUser(User user);
    }
}
