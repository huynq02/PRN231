using CmsAPI.Models;

namespace CmsAPI.Repositories
{
    public interface IQuestionRepository
    {
        Task AddQuestion(Question question);
        Task UpdateQuestion(Question question);
        Task<Question> GetQuestionById(int id);
        Task<IEnumerable<Question>> GetAllQuestions();
    }
}
