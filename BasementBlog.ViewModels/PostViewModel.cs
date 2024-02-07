using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BasementBlog.ViewModels;

public class PostViewModel : BaseViewModel
{
    private readonly IPostService postService;
    private readonly IMarkdownService markdownService;
    private readonly NavigationManager navigationManager;

    [Parameter]
    public int PostId { get; set; }

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

    private DateTime publishDate;
    public DateTime PublishDate
    {
        get => publishDate;
        set
        {
            publishDate = value;
            OnPropertyChanged();
        }
    }

    public async Task GetPostById(int postId)
    {
        var post = await postService.GetPostById(postId);
        if (post != null)
        {
            PostTitle = post.Title;
            PostBody = string.IsNullOrEmpty(post.Body) ? string.Empty : markdownService.TextToHtml(post.Body);
            PublishDate = post.PublishDate;
            PostId = postId;
        }
    }

    public void EditPost()
    {
        navigationManager.NavigateTo($"/postEditor/{PostId}");
    }
}
