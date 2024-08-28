namespace Blog.DTO;

public class WorkLogDTO
{
    public string? Id { get; set; }
    public required string Body { get; set; }
    public string LoggedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool? IsDeleted { get; set; }
}
