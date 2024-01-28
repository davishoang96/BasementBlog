namespace BasementBlog.DTO;

public class PostDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Description { get; set; }

    public DateTime PublishDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}
