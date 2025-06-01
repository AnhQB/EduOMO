using EduOMO.Base;
using EduOMO.Data.Base;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MMOEdu.Data;

namespace EduOMO.Services;
public class PostService : IPostService

{
    private readonly EduOMOContext _context;

    public PostService(EduOMOContext context)
    {
        this._context = context;
    }

    public async Task<IEnumerable<PostEntity>> GetAllPosts()
    {
        return await _context.Post.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<PostEntity>> GetFirst10Posts()
    {
        return await _context.Post.AsNoTracking().Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedAt).Take(10).ToListAsync();
    }

    public async Task<IEnumerable<PostEntity>> GetFirst5PostsNotCurrent(Guid id)
    {
        return await _context.Post.AsNoTracking().Where(x => !x.IsDeleted && id != x.Id).OrderByDescending(x => x.CreatedAt).Take(5).ToListAsync();
    }

    public async Task<PostEntity> GetPostById(Guid id)
    {
        return await _context.Post.FirstOrDefaultAsync(p => p.Id == id) ?? new PostEntity();
    }

    public async Task<PostEntity> GetPostBySlug(string slug)
    {
        return await _context.Post.FirstOrDefaultAsync(p => p.Slug == slug && !p.IsDeleted) ?? new PostEntity();
    }
    public async Task AddPost(PostEntity post)
    {
        var slug = GenerateSlugHelper.GenerateSlug(post.Title);
        post.Slug = slug;
        _context.Post.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePost(PostEntity post)
    {
        _context.Post.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeletePost(Guid id)
    {
        var post = await _context.Post.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (post == null) return;
        post.IsDeleted = true;
        _context.Post.Update(post);
        await _context.SaveChangesAsync();
    }

}
