using System.ComponentModel.DataAnnotations;

namespace Blog.Database.Models;

public class Post
{
    public int Id { get; set; }
    public int? CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required string Description { get; set; }
    public DateTime PublishDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool? IsDeleted { get; set; }
}
