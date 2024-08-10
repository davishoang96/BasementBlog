using Blog.Database;
using Microsoft.Data.Sqlite;

namespace Blog.Tests;

public class BaseDataContextTest : IDisposable
{
    internal SqliteConnection _connection;

    public BaseDataContextTest()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        using (var context = new DatabaseContext(_connection))
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
