using BasementBlog.DTO;

namespace BasementBlog.Services.Interfaces;

public interface IPostService
{
    Task<PostDTO> GetPostById(int id);
    Task<int> SaveOrUpdatePost(PostDTO post);
    Task<IEnumerable<PostDTO>> GetAllPosts();
    Task<bool> DeletePost(int id);
}
