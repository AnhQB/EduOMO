using EduOMO.Data.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MMOEdu.Data;

namespace EduOMO.Services;

public class QuestionService : IQuestionService
{
    private readonly EduOMOContext _context;

    public QuestionService(EduOMOContext context)
    {
        this._context = context;
    }
    public async Task<IEnumerable<QuestionEntity>> GetAllQuestions()
    {
        return await _context.Question.AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<QuestionEntity>> GetFirst10Questions()
    {
        return await _context.Question.AsNoTracking().Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedAt).Take(10).ToListAsync();
    }

    public async Task<QuestionEntity> GetQuestionById(Guid id)
    {
        return await _context.Question.FirstOrDefaultAsync(p => p.Id == id) ?? new QuestionEntity();
    }

    public async Task<QuestionEntity> GetQuestionBySlug(string slug)
    {
        return await _context.Question.FirstOrDefaultAsync(p => p.Slug == slug && !p.IsDeleted) ?? new QuestionEntity();
    }

    public async Task UpdateQuestion(QuestionEntity question)
    {
        _context.Question.Update(question);
        await _context.SaveChangesAsync();
    }
}
