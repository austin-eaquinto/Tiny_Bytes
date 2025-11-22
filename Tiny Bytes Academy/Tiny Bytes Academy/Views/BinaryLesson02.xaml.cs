using System.ComponentModel;
using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class BinaryLesson02 : ContentPage
{
    public BinaryLesson02(BinaryLesson02ViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    // OnAppearing is still useful to reset the lesson state when the user navigates to the page.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (this.BindingContext is BinaryLesson02ViewModel viewModel)
        {
            viewModel.InitializeComponent();
        }
    }

    // If you haven't bound the Button's Command in XAML, you can keep this:
    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is BinaryLesson02ViewModel viewModel)
        {
            viewModel.NextCommand.Execute(null);
        }
    }
}