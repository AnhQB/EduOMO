using EduOMO.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduOMO.Controllers
{
    public class QuestionMangementController : Controller
    {
        private readonly IQuestionService questionService;

        public QuestionMangementController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }
    }
}
