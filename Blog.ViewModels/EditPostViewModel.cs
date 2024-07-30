using Blog.DTO;
using Blog.Services;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Blog.ViewModels;

public class EditPostViewModel : BaseViewModel
{
    private readonly IMarkdownService markdownService;
    private readonly IFileService fileService;
    private readonly IPostService postService;
    private readonly IBlogDialogService blogDialogService;
    private readonly ISnackbar snackbar;
    private readonly NavigationManager navigationManager;

    [Parameter] public string PostId { get; set; }

    public EditPostViewModel(IBlogDialogService blogDialogService, NavigationManager navigationManager,
                             IMarkdownService markdownService, IPostService postService, IFileService fileService, ISnackbar snackbar)
    {
        this.markdownService = markdownService;
        this.postService = postService;
        this.navigationManager = navigationManager;
        this.blogDialogService = blogDialogService;
        this.snackbar = snackbar;
        this.snackbar = snackbar;
        this.fileService = fileService;
    }

    public string PostTitle { get; set; }
    public string PostDescription { get; set; }
    public string PostBody { get; set; }

    public List<CategoryDTO> Categories { get; set; }

    public async Task GetCategories()
    {
        var result = await postService.GetCategoryDTOs();
        Categories = result.ToList();
        if (!Categories.Any())
        {
            Categories.Add(new CategoryDTO
            {
                Name = "General",
            });
        }
    }

    public string PostPreview => string.IsNullOrEmpty(PostBody) ? "Type here" : markdownService.TextToHtml(PostBody);

    public async Task GetPostById(string postId)
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

        if (!string.IsNullOrEmpty(postId))
        {
            navigationManager.NavigateTo($"/viewpost/{postId}");
            snackbar.Add("Save post successfully", Severity.Success, config =>
            {
                config.CloseAfterNavigation = false;
                config.ShowTransitionDuration = 250;
                config.HideTransitionDuration = 250;
                config.DuplicatesBehavior = SnackbarDuplicatesBehavior.Prevent;
            });
            return true;
        }

        snackbar.Add("Failed to save post", Severity.Error, config =>
        {
            config.CloseAfterNavigation = false;
            config.ShowTransitionDuration = 250;
            config.HideTransitionDuration = 250;
            config.DuplicatesBehavior = SnackbarDuplicatesBehavior.Prevent;
        });

        return false;
    }

    public CategoryDTO? SelectedCategory { get; set; }

    private string newCategory { get; set; }

    public async Task<IEnumerable<CategoryDTO>> SearchCategories(string value)
    {
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

    public async Task<string> HandleFiles(object value)
    {
        if (value is null)
        {
            await blogDialogService.ShowDialog("Warning", "Cannot leave those field empty");
            return string.Empty;
        }

        return await fileService.UploadImage(value);
    }
}
