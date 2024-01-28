using BasementBlog.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BasementBlog.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<Post> Post { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasData(new()
        {
            Id = 1,
            Title = "Make the world better",
            Body = "Test",
            Description = "Test",
            ModifiedDate = DateTime.Now,
            PublishDate = DateTime.Now,
        },
        new()
        {
            Id = 2,
            Title = "AI take over the world",
            Body = "Test",
            Description = "Test",
            ModifiedDate = DateTime.Now,
            PublishDate = DateTime.Now,
        });
    }
}
