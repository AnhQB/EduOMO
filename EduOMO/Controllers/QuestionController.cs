using EduOMO.Models;
using EduOMO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EduOMO.Controllers
{
    [Route("Question")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _service;

        public QuestionController(IQuestionService service)
        {
            this._service = service;
        }
        [HttpGet("")]
        public async Task<ActionResult> Index()
        {
            var questions = await _service.GetAllQuestions();
            var result = questions.Select(x => new QuestionViewModel
            {
                Slug = x.Slug,
                Content = x.Content,
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                UpdatedBy = x.UpdatedBy,
                UpdatedAt = x.UpdatedAt
            });
            return View(result);
        }

        [HttpGet("Details/{slug}")]
        public async Task<ActionResult> Details(string slug)
        {
            var question = await _service.GetQuestionBySlug(slug);
            var result = new QuestionViewModel
            {
                Slug = question.Slug,
                Content = question.Content,
                CreatedBy = question.CreatedBy,
                CreatedAt = question.CreatedAt,
                UpdatedBy = question.UpdatedBy,
                UpdatedAt = question.UpdatedAt,
                Answers = question.Answers?.Select(y => new AnswerResponseDto
                {
                    UserName = y.UserName,
                    Content = y.Content,
                    CreatedBy = y.CreatedBy,
                    CreatedAt = y.CreatedAt,
                }).ToList() ?? new List<AnswerResponseDto>(),
            };

            var references = await _service.GetFirst5QuestionsNotCurrent(question.Id);
            ViewData["QuesReferences"] = references.Select(x => new QuestionReferenceModel
            {
                Slug = x.Slug,
                Content = x.Content?.Length > 150 ? x.Content.Substring(0, 100) : x.Content,
            });
            return View(result);
        }
    }
}
