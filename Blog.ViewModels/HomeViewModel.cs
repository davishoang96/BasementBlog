using Blog.DTO;
using Blog.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace Blog.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    private readonly IApiClient apiClient;
    private readonly NavigationManager navigationManager;

    public List<PostDTO> Posts = new List<PostDTO>();

    public List<CategoryDTO> Categoroies = new List<CategoryDTO>();

    public IAsyncRelayCommand<PostDTO> DeletePostCommand { get; }

    public HomeViewModel(NavigationManager navigationManager, IApiClient apiClient)
    {
        this.apiClient = apiClient;
        this.navigationManager = navigationManager;
        DeletePostCommand = new AsyncRelayCommand<PostDTO>(ExecuteDeletePostCommand);
    }

    public async Task GetUnclassifiedPosts()
    {
        var result = await apiClient.GetUnclassifiedPostsAsync();
        if (result.Any())
        {
            Posts = result.ToList();
        }
    }

    private async Task ExecuteDeletePostCommand(PostDTO postDTO)
    {
        //var result = await postService.DeletePost(postDTO.Id);
        //if (result)
        //{
        //    Posts.Remove(postDTO);
        //}
    }

    public async Task GetCategoriesWithPost()
    {
        var result = await apiClient.GetCategoriesWithLightPostAsync();
        if (result?.Any() == true)
        {
            Categoroies = result.ToList();
        }
    }

    public void EditPost(string postId)
    {
        navigationManager.NavigateTo($"/postEditor/{postId}");
    }

    public void ViewPost(string postId)
    {
        navigationManager.NavigateTo($"/viewpost/{postId}");
    }
}
