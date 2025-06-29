using EduOMO.Base;
using EduOMO.Data.Base;
using EduOMO.Models;
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

    public async Task AddQuestion(QuestionRequest question)
    {
        var entity = new QuestionEntity();
        var slug = GenerateSlugHelper.GenerateSlug(question.Content);

        entity.Content = question.Content;
        entity.Slug = slug;
        foreach(var item in question.Answers)
        {
            var ans = new AnswerEntity
            {
                Content = item,
                UserName = GenerateDisplayUserNameHelper.GenerateOneUsername()
            };
            entity.Answers.Add(ans);
        }

        // Optionally update audit fields
        entity.CreatedAt = DateTimeOffset.UtcNow;
        entity.CreatedBy = GenerateDisplayUserNameHelper.GenerateOneUsername();

        _context.Question.Add(entity);
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

    public async Task<bool> UpdateQuestion(QuestionRequest request)
    {
        var entity = await _context.Question.FindAsync(request.Id);

        if (entity == null)
        {
            return false;
        }

        entity.Content = request.Content;
        request.Answers = [];
        foreach (var item in request.Answers)
        {
            var ans = new AnswerEntity
            {
                Content = item,
                UserName = "abc"
            };
            entity.Answers.Add(ans);
        }

        // Optionally update audit fields
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        entity.UpdatedBy = "System(auto)";

        _context.Question.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result != 0;
    }
}
