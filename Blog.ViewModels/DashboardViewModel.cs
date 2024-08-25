//using Blog.DTO;
//using Blog.Services;
//using Blog.Services.Interfaces;
//using Microsoft.AspNetCore.Components;
//using MudBlazor;
//using System.Collections.ObjectModel;

//namespace Blog.ViewModels;

//public class DashboardViewModel : BaseViewModel
//{
//    private IPostService postService;
//    private ISnackbar snackbar;
//    private readonly NavigationManager navigationManager;

//    public DashboardViewModel(IPostService postService,
//        NavigationManager navigationManager, ISnackbar snackbar)
//    {
//        this.postService = postService;
//        this.navigationManager = navigationManager;
//        this.snackbar = snackbar;
//    }

//    private ObservableCollection<PostDTO> posts = new ObservableCollection<PostDTO>();
//    public ObservableCollection<PostDTO> Posts
//    {
//        get => posts;
//        set
//        {
//            posts = value;
//            OnPropertyChanged();
//        }
//    }

//    public List<CategoryDTO> Categories = new List<CategoryDTO>();

//    public async Task WipeAllIsDeletedPosts()
//    {
//        var result = await blogDialogService.ShowDialog("Warning", "Do you want to wipe all 'IsDeleted' posts?");
//        if (!result.Cancelled)
//        {
//            var num = await postService.WipeAllSoftDeletedPost();
//            if (num > 0)
//            {
//                var p = Posts.Where(s => s.IsDelete == null || s.IsDelete == false);
//                Posts = new ObservableCollection<PostDTO>(p);

//                // TODO: Somehow throw exception about threading. Pls investigate this.
//                //foreach(var p in Posts.Where(s => s.IsDelete == true))
//                //{
//                //    Posts.Remove(p);
//                //}

//                snackbar.Add($"Deleted {num} posts", Severity.Success, config =>
//                {
//                    config.CloseAfterNavigation = true;
//                    config.ShowTransitionDuration = 250;
//                    config.HideTransitionDuration = 250;
//                    config.DuplicatesBehavior = SnackbarDuplicatesBehavior.Prevent;
//                });
//            }
//        }
//    }

//    public async Task InitialDashboard()
//    {
//        var posts = await postService.GetAllPosts();
//        if (posts.Any())
//        {
//            Posts = new ObservableCollection<PostDTO>(posts);
//        }

//        var cate = await postService.GetCategoryDTOs();
//        if (cate.Any())
//        {
//            Categories = cate.ToList();
//        }
//    }

//    public async Task SoftDeletePost(string id)
//    {
//        if (await postService.SoftDeletePost(id))
//        {
//            var p = Posts.Single(s => s.Id == id);
//            p.IsDelete = true;
//        }
//        else
//        {
//            await blogDialogService.ShowDialog("Warning", $"Unable to delete post with id = {id}");
//        }
//    }

//    public async Task RestorePost(string id)
//    {
//        if (await postService.RestoreDeletedPost(id))
//        {
//            var p = Posts.Single(s => s.Id == id);
//            p.IsDelete = false;
//        }
//        else
//        {
//            await blogDialogService.ShowDialog("Warning", $"Unable to restore deleted post with id = {id}");
//        }
//    }

//    public void ViewPost(string postId)
//    {
//        navigationManager.NavigateTo($"/viewpost/{postId}");
//    }
//}