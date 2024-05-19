using MudBlazor;

namespace Blog.Services.Interfaces;

public interface IBlogDialogService
{
    Task<DialogResult> ShowDialog(string title, string message, string buttonText = "OK");
}
