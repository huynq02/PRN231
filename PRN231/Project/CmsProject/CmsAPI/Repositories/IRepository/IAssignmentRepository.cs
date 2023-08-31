using CmsAPI.Models;

namespace CmsAPI.Repositories.IRepository
{
    public interface IAssignmentRepository
    {
        Task CreateAssignment(Assignment assignment);
        Task<Assignment> GetAssignmentById(int id);
        Task UpdateAssignment(Assignment assignment);
    }
}
