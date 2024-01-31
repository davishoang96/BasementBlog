using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BasementBlog.Components.Pages;

public partial class ViewPost : ComponentBase
{
    [Inject]
    protected IPostService postService { get; set; } = default!;

    [Inject]
    protected IMarkdownService markdownService { get; set; } = default!;

    [Parameter] public int PostId { get; set; }

    public string PostTitle { get; set; }

    public string PostBody { get; set; }

    public DateTime PublishDate { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var post = await postService.GetPostById(PostId);
        if (post != null)
        {
            PostTitle = post.Title;
            PostBody = string.IsNullOrEmpty(post.Body) ? string.Empty : markdownService.TextToHtml(post.Body);
            PublishDate = post.PublishDate;
        }
    }
}
