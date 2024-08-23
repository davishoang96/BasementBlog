using Blog.DTO;

namespace Blog.Database.Interfaces;

public interface IPostRepository
{
    // Get
    Task<IEnumerable<PostDTO>> GetAllPosts();
    Task<IEnumerable<CategoryDTO>> GetCategoriesWithLightPostDTO();
    Task<IEnumerable<CategoryDTO>> GetCategoryDTOs();
    Task<IEnumerable<PostDTO>> GetUnclassifiedPosts();
    Task<PostDTO> GetPostById(string id);

    // Save
    Task<string> SaveOrUpdatePost(PostDTO post);

    // Delete
    Task<bool> DeletePost(string id);
    Task<bool> SoftDeletePost(string id);
    Task<int> WipeAllSoftDeletedPost();
    Task<bool> RestoreDeletedPost(string id);
}
