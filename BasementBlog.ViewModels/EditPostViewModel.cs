using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BasementBlog.ViewModels;

public class EditPostViewModel : BaseViewModel
{
    private readonly IMarkdownService markdownService;
    private readonly IPostService postService;
    private readonly IBlogDialogService blogDialogService;
    private readonly NavigationManager navigationManager;

    [Parameter] public int PostId { get; set; }

    public EditPostViewModel(IBlogDialogService blogDialogService, NavigationManager navigationManager,
        IMarkdownService markdownService, IPostService postService)
    {
        this.markdownService = markdownService;
        this.postService = postService;
        this.navigationManager = navigationManager;
        this.blogDialogService = blogDialogService;
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
            await blogDialogService.ShowDialog("Warning", "Cannot leave those field empty");
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
