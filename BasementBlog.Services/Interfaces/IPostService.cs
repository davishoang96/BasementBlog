﻿using BasementBlog.DTO;

namespace BasementBlog.Services.Interfaces;

public interface IPostService
{
    Task<PostDTO> GetPostById(string id);
    Task<string> SaveOrUpdatePost(PostDTO post);
    Task<IEnumerable<PostDTO>> GetAllPosts();
    Task<bool> DeletePost(string id);
    Task<IEnumerable<CategoryDTO>> GetCategoriesWithLightPostDTO();
    Task<IEnumerable<CategoryDTO>> GetCategoryDTOs();
    Task<IEnumerable<PostDTO>> GetUnclassifiedPosts();
    Task<bool> SoftDeletePost(string id);
}
