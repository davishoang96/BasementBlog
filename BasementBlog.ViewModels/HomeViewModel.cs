using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace BasementBlog.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    private readonly IPostService postService;
    private readonly NavigationManager navigationManager;

    public List<PostDTO> Posts = new List<PostDTO>();

    public IAsyncRelayCommand<PostDTO> DeletePostCommand { get; }

    public HomeViewModel(NavigationManager navigationManager, IPostService postService)
    {
        this.postService = postService;
        this.navigationManager = navigationManager;

        DeletePostCommand = new AsyncRelayCommand<PostDTO>(ExecuteDeletePostCommand);
    }


    public async Task GetPosts()
    {
        var result = await postService.GetAllPosts();
        if (result.Any())
        {
            Posts = result.ToList();
        }
    }

    private async Task ExecuteDeletePostCommand(PostDTO postDTO)
    {
        var result = await postService.DeletePost(postDTO.Id);
        if (result)
        {
            Posts.Remove(postDTO);
        }
    }

    public void EditPost(int postId)
    {
        navigationManager.NavigateTo($"/postEditor/{postId}");
    }

    public void ViewPost(int postId)
    {
        navigationManager.NavigateTo($"/viewpost/{postId}");
    }
}
