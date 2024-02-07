namespace BasementBlog.Services.Interfaces;

public interface IBlogDialogService
{
    Task ShowDialog(string title, string message, string buttonText = "OK");
}
