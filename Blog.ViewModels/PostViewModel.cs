using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blog.ViewModels;

public class PostViewModel : BaseViewModel
{
    private readonly IPostService postService;
    private readonly IMarkdownService markdownService;
    private readonly NavigationManager navigationManager;

    [Parameter]
    public string PostId { get; set; }

    public PostViewModel(IDialogService dialogService, NavigationManager navigationManager,
        IPostService postService, IMarkdownService markdownService)
    {
        this.navigationManager = navigationManager;
        this.postService = postService;
        this.markdownService = markdownService;
    }

    private string postTitle;
    public string PostTitle
    {
        get => postTitle;
        set
        {
            postTitle = value;
            OnPropertyChanged();
        }
    }

    private string postBody;
    public string PostBody
    {
        get => postBody;
        set
        {
            postBody = value;
            OnPropertyChanged();
        }
    }

    private string publishDate;
    public string PublishDate
    {
        get => publishDate;
        set
        {
            publishDate = value;
            OnPropertyChanged();
        }
    }

    public async Task GetPostById(string postId)
    {
        var post = await postService.GetPostById(postId);
        if (post != null)
        {
            PostTitle = post.Title;
            PostBody = string.IsNullOrEmpty(post.Body) ? string.Empty : markdownService.TextToHtml(post.Body);
            PublishDate = post.PublishDate.ToString("dd/MM/yyyy");
            PostId = postId;
        }
    }

    public void EditPost()
    {
        navigationManager.NavigateTo($"/postEditor/{PostId}");
    }
}
