using CommunityToolkit.Mvvm.ComponentModel;

namespace Blog.ViewModels;

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
