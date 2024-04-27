using BasementBlog.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BasementBlog.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions) { }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<WorkLog> WorkLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Post>()
            .HasIndex(s => s.Id);

        modelBuilder.Entity<WorkLog>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<WorkLog>()
            .HasIndex(s => s.LoggedDate)
            .HasDatabaseName("IX_Unique_LoggedDate")
            .IsUnique();
            
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Category);
        modelBuilder.Entity<Category>()
            .HasIndex(p => p.CategoryId);

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
