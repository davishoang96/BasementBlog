using BasementBlog.DTO;
using System.Net.Http.Json;

namespace DevBlog.Shared;

public interface IPostService
{
    Task<IEnumerable<PostDTO>> GetPostsAsync();
}

public class PostService : IPostService
{
    private readonly HttpClient _httpClient;

    public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<PostDTO>> GetPostsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<PostDTO>>("GetPost");
    }
}
