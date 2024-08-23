namespace Blog.Database.Models;

public class WorkLog
{
    public int Id { get; set; }
    public required string Body { get; set; }
    public required string LoggedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool? IsDeleted { get; set; }
}
