using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using BasementBlog;
using BasementBlog.Views;

namespace BasementBlog.ViewModels;

public class EditPostViewModel : BaseViewModel
{
    private readonly IMarkdownService markdownService;
    private readonly IPostService postService;
    private readonly IDialogService dialogService;
    private readonly NavigationManager navigationManager;

    [Parameter] public int PostId { get; set; }

    public EditPostViewModel(IDialogService dialogService, NavigationManager navigationManager, IMarkdownService markdownService, IPostService postService)
    {
        this.markdownService = markdownService;
        this.postService = postService;
        this.navigationManager = navigationManager;
        this.dialogService = dialogService;
    }

    public string PostTitle { get; set; }
    public string PostDescription { get; set; }
    public string PostBody { get; set; }

    public string PostPreview => string.IsNullOrEmpty(PostBody) ? "Type here" : markdownService.TextToHtml(PostBody);

    public async Task GetPostById(int postId)
    {
        if (postId == default)
        {
            return;
        }

        PostId = postId;

        var post = await postService.GetPostById(postId);
        if (post != null)
        {
            PostTitle = post.Title;
            PostDescription = post.Description;
            PostBody = post.Body;
        }
    }

    public async Task<bool> SaveOrUpdatePostCommand()
    {
        if (string.IsNullOrEmpty(PostTitle) || string.IsNullOrEmpty(PostDescription) || string.IsNullOrEmpty(PostBody))
        {
            await dialogService.ShowAsync<Dialog>("Must enter something", new DialogOptions { CloseOnEscapeKey = true });
            return false;
        }

        var postId = await postService.SaveOrUpdatePost(new PostDTO
        {
            Id = PostId,
            Title = PostTitle,
            Description = PostDescription,
            Body = PostBody,
        });

        if (postId > 0)
        {
            navigationManager.NavigateTo($"/viewpost/{postId}");
            return true;
        }

        return false;
    }
}
