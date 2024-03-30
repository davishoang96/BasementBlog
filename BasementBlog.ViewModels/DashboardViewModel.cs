using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;

namespace BasementBlog.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    private IPostService postService;
    private IBlogDialogService blogDialogService;
    public DashboardViewModel(IPostService postService, IBlogDialogService blogDialogService)
    {
        this.blogDialogService = blogDialogService;
        this.postService = postService;
    }

    public List<PostDTO> Posts = new List<PostDTO>();
    public List<CategoryDTO> Categories = new List<CategoryDTO>();

    public async Task InitialDashboard()
    {
        var posts = await postService.GetAllPosts();
        if (posts.Any())
        {
            Posts = posts.ToList();
        }

        var cate = await postService.GetCategoryDTOs();
        if (cate.Any())
        {
            Categories = cate.ToList();
        }
    }

    public async Task SoftDeletePost(string id)
    {
        if (await postService.SoftDeletePost(id))
        {
            var p = Posts.Single(s => s.Id == id);
            p.IsDelete = true;
        }
        else
        {
            await blogDialogService.ShowDialog("Warning", $"Unable to delete post with id = {id}");
        }
    }
}