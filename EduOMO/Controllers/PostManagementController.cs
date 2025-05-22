using EduOMO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return View();
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
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: PostManagementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
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
