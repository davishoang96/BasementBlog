using Microsoft.AspNetCore.Components;

namespace BasementBlog.Components.Pages;

public partial class PostEditor : ComponentBase
{
    public string PostTitle { get; set; }
    public string PostDescription { get; set; }
    public string PostContent { get; set; }
}
