using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Graph.DeviceManagement.AuditEvents.GetAuditCategories;
using Microsoft.Graph.Models;
using System.Diagnostics.Metrics;

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

    public IEnumerable<CategoryDTO> Categories { get; set; }

    public async Task GetCategories()
    {
        Categories = await postService.GetCategoryDTOs();
    }

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
            SelectedCategory = Categories.SingleOrDefault(s => s.CategoryId == post.CategoryId);
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
            CategoryId = SelectedCategory != null ? SelectedCategory.CategoryId : 0,
            CategoryName = SelectedCategory != null ? SelectedCategory.Name : newCategory,
            Body = PostBody,
        });

        if (postId > 0)
        {
            navigationManager.NavigateTo($"/viewpost/{postId}");
            return true;
        }
        return false;
    }

    public Func<CategoryDTO, string?> DisplayValue = item => item is null ? null : item.Name;

    public CategoryDTO? SelectedCategory { get; set; }

    private string newCategory { get; set; }

    public async Task<IEnumerable<CategoryDTO>> SearchCategories(string value)
    {
        if (!Categories.Any())
        {
            return null;
        }

        if (string.IsNullOrEmpty(value))
        {
            return Categories;
        }

        var result = Categories.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        if (!result.Any())
        {
            SelectedCategory = null;
            newCategory = value;
        }

        return result;
    }
}
