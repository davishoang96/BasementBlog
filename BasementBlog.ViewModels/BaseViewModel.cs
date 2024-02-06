using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace BasementBlog.ViewModels;

public class BaseViewModel : ObservableObject
{
    private bool isBusy;
    public bool IsBusy
    {
        get => isBusy;
        set
        {
            isBusy = value;
            OnPropertyChanged(nameof(IsBusy));
        }
    }
}
