namespace Tiny_Bytes_Academy.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
	}

    private async void OnNavigateButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"///{nameof(BinaryLesson01)}");
    }
}