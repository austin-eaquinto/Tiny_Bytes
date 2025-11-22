using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

// In MenuPage.xaml.cs
public partial class MenuPage : ContentPage
{
    // The DI system will automatically provide the MenuPageViewModel instance.
    public MenuPage(MenuPageViewModel viewModel)
    {
        InitializeComponent();

        // ? Set the BindingContext here, in C#.
        this.BindingContext = viewModel;
    }
}