using System.Windows.Input;
using Tiny_Bytes_Academy.Views;

namespace Tiny_Bytes_Academy.ViewModels;

public class StartPageViewModel : BaseViewModel
{
    public ICommand GoToMenuPageCommand { get; }
    public ICommand GoToSettingsPageCommand { get; }

    public StartPageViewModel()
    {
        GoToMenuPageCommand = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(MenuPage)}"));
        GoToSettingsPageCommand = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(SettingsPage)}"));
    }
}