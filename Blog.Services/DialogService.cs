using Blog.Services.Interfaces;
using Blog.Views;
using MudBlazor;

namespace Blog.Services;

public class BlogDialogService : IBlogDialogService
{
    private readonly IDialogService dialogService;
    public BlogDialogService(IDialogService dialogService)
    {
        this.dialogService = dialogService;
    }

    public async Task<DialogResult> ShowDialog(string title, string message, string buttonText = "OK")
    {
        var parameters = new DialogParameters<Dialog>
        {
            { x => x.ContentText, message },
            { x => x.ButtonText, buttonText },
            { x => x.Color, Color.Primary }
        };

        var dialog = await dialogService.ShowAsync<Dialog>(title, parameters, new DialogOptions { CloseOnEscapeKey = true });
        return await dialog.Result;
    }
}
