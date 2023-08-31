using CmsAPI.Models;

namespace CmsAPI.Repositories
{
    public interface IStudentAssignmentRepository
    {
        Task AddSubmission(StudentAssignment studentAssignment);
        Task UpdateSubmission(StudentAssignment studentAssignment);
        Task<StudentAssignment> GetSubmissionById(int id);
    }
}
