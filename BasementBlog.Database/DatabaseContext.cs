using Blog.Database.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Blog.Database;

public class DatabaseContext : DbContext
{
    private SqliteConnection connection;
    private string dbFile = "BlogBasement.db";

    public DatabaseContext() { }
    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions) { }

    public DatabaseContext(string databaseFile)
    {
        if (!string.IsNullOrEmpty(databaseFile))
        {
            dbFile = databaseFile;
        }
    }

    public DatabaseContext(SqliteConnection sqliteConnection)
    {
        if (!string.IsNullOrEmpty(sqliteConnection?.DataSource))
        {
            dbFile = sqliteConnection.DataSource;
        }

        connection = sqliteConnection;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        connection ??= InitializeSQLiteConnection(dbFile);
        optionsBuilder.UseSqlite(connection);
    }

    private static SqliteConnection InitializeSQLiteConnection(string databaseFile)
    {
        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = databaseFile,
            Password = "@j32[f09q;4gq43Q#$VC"
        };
        return new SqliteConnection(connectionString.ToString());
    }

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
