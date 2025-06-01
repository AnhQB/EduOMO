using MMOEdu.Data;

namespace EduOMO.Services;

public interface IPostService
{
    Task<IEnumerable<PostEntity>> GetAllPosts();
    Task<IEnumerable<PostEntity>> GetFirst10Posts();
    Task<IEnumerable<PostEntity>> GetFirst5PostsNotCurrent(Guid id);
    Task<PostEntity> GetPostById(Guid id);
    Task<PostEntity> GetPostBySlug(string slug);
    Task AddPost(PostEntity post);
    Task UpdatePost(PostEntity post);
    Task SoftDeletePost(Guid id);
}
