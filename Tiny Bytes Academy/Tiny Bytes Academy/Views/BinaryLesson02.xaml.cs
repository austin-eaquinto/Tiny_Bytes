using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class BinaryLesson02 : ContentPage
{
	public BinaryLesson02()
	{
		InitializeComponent();
        BindingContext = new BinaryLesson02ViewModel();
    }

    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is BinaryLesson02ViewModel viewModel)
        {
            viewModel.NextCommand.Execute(null);
        }
    }
}