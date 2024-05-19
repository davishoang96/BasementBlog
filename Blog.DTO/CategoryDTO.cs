using Blog.DTO;

namespace Blog.Services;

public class CategoryDTO
{
    public int CategoryId { get; set; }
    public required string Name { get; set; }
    public IEnumerable<PostDTO>? PostDTOs { get; set; }
}
