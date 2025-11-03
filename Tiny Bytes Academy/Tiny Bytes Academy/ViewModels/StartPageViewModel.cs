using System.Windows.Input;
using Tiny_Bytes_Academy.Views;

namespace Tiny_Bytes_Academy.ViewModels;

public class StartPageViewModel : BaseViewModel
{
    public ICommand NavigateCommand { get; }

    public StartPageViewModel()
    {
        NavigateCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(MenuPage));
        });
    }
}