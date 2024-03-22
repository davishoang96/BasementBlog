using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace BasementBlog.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    private readonly IPostService postService;
    private readonly ISqidService sqidService;
    private readonly NavigationManager navigationManager;

    public List<PostDTO> Posts = new List<PostDTO>();

    public List<CategoryDTO> Categoroies = new List<CategoryDTO>();

    public IAsyncRelayCommand<PostDTO> DeletePostCommand { get; }

    public HomeViewModel(NavigationManager navigationManager, IPostService postService, ISqidService sqidService)
    {
        this.postService = postService;
        this.navigationManager = navigationManager;
        this.sqidService = sqidService;

        DeletePostCommand = new AsyncRelayCommand<PostDTO>(ExecuteDeletePostCommand);
    }


    public async Task GetUnclassifiedPosts()
    {
        var result = await postService.GetUnclassifiedPosts();
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

    public async Task GetCategoriesWithPost()
    {
        var result = await postService.GetCategoriesWithLightPostDTO();
        if (result?.Any() == true)
        {
            Categoroies = result.ToList();
        }
    }

    public void EditPost(int postId)
    {
        navigationManager.NavigateTo($"/postEditor/{sqidService.EncryptId(postId)}");
    }

    public void ViewPost(int postId)
    {
        navigationManager.NavigateTo($"/viewpost/{sqidService.EncryptId(postId)}");
    }
}
