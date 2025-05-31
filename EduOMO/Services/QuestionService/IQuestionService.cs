using MMOEdu.Data;

namespace EduOMO.Services;

public interface IQuestionService
{
    Task<IEnumerable<QuestionEntity>> GetAllQuestions();
    Task<IEnumerable<QuestionEntity>> GetFirst10Questions();
    Task<QuestionEntity> GetQuestionById(Guid id);
    Task<QuestionEntity> GetQuestionBySlug(string slug);
    Task UpdateQuestion(QuestionEntity question);
}
