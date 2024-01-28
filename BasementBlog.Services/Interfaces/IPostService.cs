using BasementBlog.DTO;

namespace BasementBlog.Services.Interfaces;

public interface IPostService
{
    Task<PostDTO> GetPostById(int id);
    Task<bool> SaveOrUpdatePost(PostDTO post);
    Task<IEnumerable<PostDTO>> GetAllPosts();
}
