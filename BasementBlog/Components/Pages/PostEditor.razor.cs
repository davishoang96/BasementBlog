using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BasementBlog.Components.Pages;

public partial class PostEditor : ComponentBase
{
    [Inject]
    protected IMarkdownService markdownService { get; set; } = default!;

    [Inject]
    protected IPostService postService { get; set; } = default!;

    [Inject]
    protected IDialogService dialogService { get; set; } = default!;

    [Inject]
    protected NavigationManager navigationManager { get; set; } = default!;

    [Parameter] public int PostId { get; set; }

    public string PostTitle { get; set; }
    public string PostDescription { get; set; }
    public string PostBody { get; set; }

    public string PostPreview => string.IsNullOrEmpty(PostBody) ? "Type here" : markdownService.TextToHtml(PostBody);

    protected override async Task OnInitializedAsync()
    {
        if (PostId == default)
        {
            return;
        }

        var post = await postService.GetPostById(PostId);
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
