using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BasementBlog.ViewModels;

public class AdminDashboardViewModel : BaseViewModel
{
    private IPostService postService;
    public AdminDashboardViewModel(IPostService postService) 
    {
        this.postService = postService;
    }

    private IList<PostDTO> posts;
    public List<PostDTO> Posts = new List<PostDTO>();

    public async Task GetUnclassifiedPosts()
    {
        var result = await postService.GetUnclassifiedPosts();
        if (result.Any())
        {
            Posts = result.ToList();
        }
    }
}