using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BasementBlog.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    protected IPostService postService { get; set; } = default!;

    public IEnumerable<PostDTO> Posts { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Posts = await postService.GetAllPosts();
    }
}
