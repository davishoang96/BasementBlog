using Blog.DTO;
using System.Net.Http.Json;

namespace Blog.Client.Services;

public interface IPostServices
{
    Task<IEnumerable<PostDTO>> GetPostsAsync();
}

public class PostServices : IPostServices
{
    private readonly HttpClient _httpClient;

    public PostServices(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BlogAppApi");
    }

    public async Task<IEnumerable<PostDTO>> GetPostsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<PostDTO>>("post/GetPosts");
    }
}
