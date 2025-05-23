﻿using Microsoft.Extensions.Hosting;
using MMOEdu.Data;

namespace EduOMO.Services;

public interface IPostService
{
    Task<IEnumerable<PostEntity>> GetAllPosts();
    Task<PostEntity> GetPostById(Guid id);
    Task<PostEntity> GetPostBySlug(string slug);
    Task UpdatePost(PostEntity post);
}
