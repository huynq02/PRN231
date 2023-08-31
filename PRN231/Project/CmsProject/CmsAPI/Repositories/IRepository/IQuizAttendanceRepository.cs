using CmsAPI.Models;

namespace CmsAPI.Repositories
{
    public interface IQuizAttendanceRepository
    {
        Task AddQuizResult(QuizAttendance quizAttendance);
        Task<QuizAttendance> GetAttempt(int studentId, int quizId);
        Task DeleteAttempt(QuizAttendance quizAttendance);
    }
}
