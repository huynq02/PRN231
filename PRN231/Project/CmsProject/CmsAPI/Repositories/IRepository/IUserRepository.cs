using CmsAPI.Models;

namespace CmsAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
    }
}
