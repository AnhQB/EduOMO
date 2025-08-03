using EduOMO.Base;
using EduOMO.Base.Dto;
using EduOMO.Models;
using EduOMO.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace EduOMO.Controllers
{
    [Route("sitemap")]
    public class SiteMapController : Controller
    {
        private readonly IPostService postService;
        private readonly IQuestionService questionService;

        public SiteMapController(IPostService postService, IQuestionService questionService)
        {
            this.postService = postService;
            this.questionService = questionService;
        }

        [HttpGet("sitemap.xml")]
        public async Task<ActionResult> Index()
        {
            var postCount = await postService.CountPosts();
            var questionCount = await questionService.CountQuestions();
            var items = new List<SitemapIndexNode>();

            int postPages = (int)Math.Ceiling((double)postCount / BaseConfig.SiteMapPageSize);
            int qPages = (int)Math.Ceiling((double)questionCount / BaseConfig.SiteMapPageSize);

            items.Add(new SitemapIndexNode { Loc = $"{Request.Scheme}://{Request.Host}/sitemap/sitemap-root.xml", LastMod = DateTimeOffset.UtcNow });

            for (int i = 1; i <= postPages; i++)
            {
                var loc = $"{Request.Scheme}://{Request.Host}/sitemap/sitemap-{GenerateSiteMapHelper.POSTNAVIGATION}{i}.xml";
                items.Add(new SitemapIndexNode { Loc = loc, LastMod = DateTimeOffset.UtcNow });
            }
                
            for (int i = 1; i <= qPages; i++)
            {
                var loc = $"{Request.Scheme}://{Request.Host}/sitemap/sitemap-{GenerateSiteMapHelper.QUESTIONNAVIGATION}{i}.xml";
                items.Add(new SitemapIndexNode { Loc = loc, LastMod = DateTimeOffset.UtcNow });
            }

            var xml = GenerateSiteMapHelper.GetSiteMap(items);
            return Content(xml, "application/xml");
        }

        [HttpGet("sitemap-root.xml")]
        public async Task<ActionResult> IndexRoot()
        {
            var request = $"{Request.Scheme}://{Request.Host}";
            var xml = GenerateSiteMapHelper.GetSiteMapRoot(request);
            return Content(xml, "application/xml");
        }

        [HttpGet("sitemap-{child}.xml")]
        public async Task<ActionResult> GetChild(string child)
        {
            var request = $"{Request.Scheme}://{Request.Host}";

            if (child.Contains(GenerateSiteMapHelper.POSTNAVIGATION))
            {
                var index = child.Substring(GenerateSiteMapHelper.POSTNAVIGATION.Count());
                var list = await postService.GetAllPosts(int.TryParse(index, out var result) ? result : 1);
                var xml = GenerateSiteMapHelper.GetSiteMapElement(request, list);
                return Content(xml, "application/xml");
            }
            else if (child.Contains(GenerateSiteMapHelper.QUESTIONNAVIGATION))
            {
                var index = child.Substring(GenerateSiteMapHelper.QUESTIONNAVIGATION.Count());
                var list = await questionService.GetAllQuestions(int.TryParse(index, out var result) ? result : 1);
                var xml = GenerateSiteMapHelper.GetSiteMapElement(request, list);
                return Content(xml, "application/xml");
            }
            else
            {
                var xml = GenerateSiteMapHelper.GetSiteMapRoot(request);
                return Content(xml, "application/xml");
            }
        }
    }
}
