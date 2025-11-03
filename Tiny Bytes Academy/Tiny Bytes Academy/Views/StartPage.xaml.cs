namespace Tiny_Bytes_Academy.Views;

public partial class StartPage : ContentPage
{
    public StartPage()
    {
        InitializeComponent();
    }

    private async void OnNavigateButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"///{nameof(MenuPage)}");
    }
}
