using EduOMO.Models;
using MMOEdu.Data;

namespace EduOMO.Services;

public interface IQuestionService
{
    Task<IEnumerable<QuestionEntity>> GetAllQuestions();
    Task<IEnumerable<QuestionEntity>> GetFirst10Questions();
    Task<IEnumerable<QuestionEntity>> GetFirst5QuestionsNotCurrent(Guid id);
    Task<QuestionEntity> GetQuestionById(Guid id);
    Task<QuestionEntity> GetQuestionBySlug(string slug);
    Task AddQuestion(QuestionRequest question);
    Task<bool> UpdateQuestion(QuestionRequest question);
    Task SoftDeleteQuestion(Guid id);
}
