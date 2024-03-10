using BasementBlog.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BasementBlog.Tests.RepoTest;

public class BaseDataContextTest : IDisposable
{
    internal SqliteConnection _connection;
    internal DbContextOptions<DatabaseContext> _dbContextOptions;

    public BaseDataContextTest()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(_connection).Options;

        using (var context = new DatabaseContext(_dbContextOptions))
        {
            context.Database.EnsureCreated();
        }
    }

    #region IDisposable Support
    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _connection.Close();
                _connection.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }
    #endregion
}
