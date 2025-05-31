using EduOMO.Data.Base;
using EduOMO.Models;
using EduOMO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EduOMO.Controllers
{
    [Route("Post")]
    public class PostController : Controller
    {
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            this._service = service;
        }
        // GET: PostController
        [HttpGet("")]
        public async Task<ActionResult> Index()
        {
            var posts = await _service.GetAllPosts();
            var result = posts.Select(x => new PostViewModel
            {
                Slug = x.Slug,
                Title = x.Title,
                Content = x.Content,
                Keyword = x.Keyword,
                Document = x.Document,
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                UpdatedBy = x.UpdatedBy,
                UpdatedAt = x.UpdatedAt
            });
            return View(result);
        }

        // GET: PostController/Details/5
        [HttpGet("Details/{slug}")]
        public async Task<ActionResult> Details(string slug)
        {
            var post = await _service.GetPostBySlug(slug);
            var result = new PostViewModel
            {
                Slug = post.Slug,
                Title = post.Title,
                Content = post.Content,
                Keyword = post.Keyword,
                CreatedBy = post.CreatedBy,
                CreatedAt = post.CreatedAt,
                UpdatedBy = post.UpdatedBy,
                UpdatedAt = post.UpdatedAt
            };
            return View(result);
        }
    }
}
