using EduOMO.Models;
using EduOMO.Services;
using Microsoft.AspNetCore.Mvc;
using MMOEdu.Data;

namespace EduOMO.Controllers
{
    public class QuestionManagementController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionManagementController(IQuestionService questionService)
        {
            this._questionService = questionService;
        }

        public async Task<ActionResult> Index()
        {
            var questions = await _questionService.GetAllQuestions();
            foreach (var item in questions)
            {
                item.Content = item.Content?.Length > 150 ? item.Content.Substring(0, 250) + "..." : item.Content;
            }
            return View(questions);
        }

        public ActionResult Create()
        {
            return View(new QuestionEntity());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuestionRequest request)
        {
            try
            {
                await _questionService.AddQuestion(request);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var question = await _questionService.GetQuestionById(id);
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QuestionRequest request)
        {
            try
            {
                await _questionService.UpdateQuestion(request);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _questionService.SoftDeleteQuestion(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }
    }
}
