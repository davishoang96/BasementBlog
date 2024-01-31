using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BasementBlog.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    protected IPostService postService { get; set; } = default!;

    [Inject]
    protected NavigationManager navigationManager { get; set; } = default!;

    public List<PostDTO> Posts { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await postService.GetAllPosts();
        if (result.Any())
        {
            Posts = result.ToList();
        }
    }

    public async void DeletePostCommand(PostDTO postDTO)
    {
        var result = await postService.DeletePost(postDTO.Id);
        if (result)
        {
            Posts.Remove(postDTO);
        }
    }

    public async void ViewPostById(int postId)
    {
        navigationManager.NavigateTo($"/viewpost/{postId}");
    }
}
