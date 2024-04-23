namespace BasementBlog.DTO;

public class WorkLogDTO
{
    public int Id { get; set; }
    public required string Body { get; set; }
    public DateTime LoggedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool? IsDeleted { get; set; }
}
