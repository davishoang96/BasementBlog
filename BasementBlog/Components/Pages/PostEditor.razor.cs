using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using BasementBlog.DTO;

namespace BasementBlog.Components.Pages;

public partial class PostEditor : ComponentBase
{
    [Inject]
    protected IMarkdownService markdownService { get; set; } = default!;

    [Inject]
    protected IPostService postService { get; set; } = default!;

    public string PostTitle { get; set; }
    public string PostDescription { get; set; }
    public string PostContent { get; set; }

    public string PostPreview => string.IsNullOrEmpty(PostContent) ? "Type here" : markdownService.TextToHtml(PostContent);

    public async Task<bool> SaveOrUpdatePostCommand()
    {
        var result = await postService.SaveOrUpdatePost(new PostDTO
        {
            Title = PostTitle,
            Description = PostDescription,
            Body = PostContent,
        });

        if(result)
        {
            return true;
        }

        return false;
    }
}
