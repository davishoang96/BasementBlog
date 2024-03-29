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

    private IList<PostDTO> posts;
    public List<PostDTO> Posts = new List<PostDTO>();

    public async Task GetAllPosts()
    {
        var result = await postService.GetAllPosts();
        if (result.Any())
        {
            Posts = result.ToList();
        }
    }

    public async Task DeletePost(string id)
    {
        if (await postService.DeletePost(id))
        {
            Posts.Remove(Posts.Single(s => s.Id == id));
        }
        else
        {
            await blogDialogService.ShowDialog("Warning", $"Unable to delete post with id = {id}");
        }
    }
}