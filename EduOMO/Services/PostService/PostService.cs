using EduOMO.Data.Base;
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
        return await _context.Post.AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<PostEntity>> GetFirst10Posts()
    {
        return await _context.Post.AsNoTracking().Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedAt).Take(10).ToListAsync();
    }

    public async Task<PostEntity> GetPostById(Guid id)
    {
        return await _context.Post.FirstOrDefaultAsync(p => p.Id == id) ?? new PostEntity();
    }

    public async Task<PostEntity> GetPostBySlug(string slug)
    {
        return await _context.Post.FirstOrDefaultAsync(p => p.Slug == slug && !p.IsDeleted) ?? new PostEntity();
    }

    public async Task UpdatePost(PostEntity post)
    {
        _context.Post.Update(post);
        await _context.SaveChangesAsync();
    }
}
