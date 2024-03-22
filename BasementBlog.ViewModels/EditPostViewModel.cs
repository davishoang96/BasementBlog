using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BasementBlog.ViewModels;

public class EditPostViewModel : BaseViewModel
{
    // TODO: Move Sqids logic to repo
    private readonly ISqidService sqidService;
    private readonly IMarkdownService markdownService;
    private readonly IPostService postService;
    private readonly IBlogDialogService blogDialogService;
    private readonly NavigationManager navigationManager;

    [Parameter] public string PostId { get; set; }

    public EditPostViewModel(IBlogDialogService blogDialogService, NavigationManager navigationManager,
                             IMarkdownService markdownService, IPostService postService, ISqidService sqidService)
    {
        this.markdownService = markdownService;
        this.postService = postService;
        this.navigationManager = navigationManager;
        this.blogDialogService = blogDialogService;
        this.sqidService = sqidService;
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

    // TODO: Clean up
    private int id { get; set; }

    public async Task GetPostById(string postId)
    {
        if (postId == default)
        {
            return;
        }

        id = sqidService.DecryptId(postId);
        var post = await postService.GetPostById(id);
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
            Id = id,
            Title = PostTitle,
            Description = PostDescription,
            CategoryId = SelectedCategory != null ? SelectedCategory.CategoryId : 0,
            CategoryName = SelectedCategory != null ? SelectedCategory.Name : newCategory,
            Body = PostBody,
        });

        if (postId > 0)
        {
            navigationManager.NavigateTo($"/viewpost/{sqidService.EncryptId(postId)}");
            return true;
        }
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
}
