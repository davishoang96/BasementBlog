using BasementBlog.Services.Interfaces;
using BasementBlog.Views;
using MudBlazor;

namespace BasementBlog.Services;

public class BlogDialogService : IBlogDialogService
{
    private readonly IDialogService dialogService;
    public BlogDialogService(IDialogService dialogService)
    {
        this.dialogService = dialogService;
    }

    public async Task ShowDialog(string title, string message, string buttonText = "OK")
    {
        var parameters = new DialogParameters<Dialog>
        {
            { x => x.ContentText, message },
            { x => x.ButtonText, buttonText },
            { x => x.Color, Color.Primary }
        };

        await dialogService.ShowAsync<Dialog>(title, parameters, new DialogOptions { CloseOnEscapeKey = true });
    }
}
