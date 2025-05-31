using EduOMO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMOEdu.Data;

namespace EduOMO.Controllers
{
    [Route("PostManagement")]
    public class PostManagementController : Controller
    {
        private readonly IPostService _postService;

        public PostManagementController(IPostService postService)
        {
            this._postService = postService;
        }
        [HttpGet("")]
        public async Task<ActionResult> Index()
        {
            var posts = await _postService.GetAllPosts();
            return View(posts);
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View(new PostEntity());
        }

        // POST: PostManagementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostManagementController/Edit/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var post = await _postService.GetPostById(id);
            return View(post);
        }

        // POST: PostManagementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PostEntity request)
        {
            try
            {
                var post = await _postService.GetPostById(request.Id); // or _context.Set<PostEntity>()

                if (post == null)
                {
                    return NotFound();
                }

                post.Title = request.Title;
                post.Content = request.Content;
                post.Keyword = request.Keyword;
                post.Document = request.Document;

                // Optionally update audit fields
                post.UpdatedAt = DateTimeOffset.UtcNow;
                post.UpdatedBy = User.Identity?.Name;

                await _postService.UpdatePost(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostManagementController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: PostManagementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
