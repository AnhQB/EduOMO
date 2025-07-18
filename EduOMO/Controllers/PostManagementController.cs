﻿using EduOMO.Base;
using EduOMO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMOEdu.Data;

namespace EduOMO.Controllers
{
    public class PostManagementController : Controller
    {
        private readonly IPostService _postService;

        public PostManagementController(IPostService postService)
        {
            this._postService = postService;
        }
        public async Task<ActionResult> Index()
        {
            var posts = await _postService.GetAllPosts();
            foreach (var item in posts)
            {
                item.Content = item.Content?.Length > 150 ? item.Content.Substring(0, 250) + "..." : item.Content;
            }
            return View(posts);
        }

        public ActionResult Create()
        {
            return View(new PostEntity());
        }

        // POST: PostManagementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostEntity request)
        {
            try
            {
                await _postService.AddPost(request);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostManagementController/Edit/5
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
                post.DescriptionOG = request.Content!.StripHtmlTags();

                // Optionally update audit fields
                post.UpdatedAt = DateTimeOffset.UtcNow;
                post.UpdatedBy = "Sytem(auto)";

                await _postService.UpdatePost(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostManagementController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _postService.SoftDeletePost(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            
        }
    }
}
