namespace Blog.DTO;

public class PostDTO
{
    public string? Id { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required string Description { get; set; }

    public DateTime PublishDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool? IsDelete { get; set; }
}
