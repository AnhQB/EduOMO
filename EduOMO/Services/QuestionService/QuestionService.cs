using EduOMO.Base;
using EduOMO.Data.Base;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<QuestionEntity>> GetFirst5QuestionsNotCurrent(Guid id)
    {
        return await _context.Question.AsNoTracking().Where(x => !x.IsDeleted && id != x.Id).OrderByDescending(x => x.CreatedAt).Take(5).ToListAsync();
    }

    public async Task<QuestionEntity> GetQuestionById(Guid id)
    {
        return await _context.Question.FirstOrDefaultAsync(p => p.Id == id) ?? new QuestionEntity();
    }

    public async Task<QuestionEntity> GetQuestionBySlug(string slug)
    {
        return await _context.Question.FirstOrDefaultAsync(p => p.Slug == slug && !p.IsDeleted) ?? new QuestionEntity();
    }

    public async Task AddQuestion(QuestionEntity question)
    {
        var slug = GenerateSlugHelper.GenerateSlug(question.Content);
        question.Slug = slug;
        _context.Question.Add(question);
        await _context.SaveChangesAsync();
    }
    public async Task SoftDeleteQuestion(Guid id)
    {
        var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (question == null) return;
        question.IsDeleted = true;
        _context.Question.Update(question);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQuestion(QuestionEntity question)
    {
        _context.Question.Update(question);
        await _context.SaveChangesAsync();
    }
}
