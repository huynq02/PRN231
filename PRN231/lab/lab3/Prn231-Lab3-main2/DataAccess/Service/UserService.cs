using BussinessObject.Models;
using DataAccess.Repository.Interface;
using DataAccess.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<User> GetByIdAsync(object id)
        {
            return (_userRepository.GetByIdAsync(id));

        }

        public Task<User> GetByUsernameAsync(string username)
        {

             return _userRepository.GetByUsernameAsync(username);

        }

        public Task<User> SaveUser(User user)
        {
           return _userRepository.SaveUser(user);
        }
    }
}
