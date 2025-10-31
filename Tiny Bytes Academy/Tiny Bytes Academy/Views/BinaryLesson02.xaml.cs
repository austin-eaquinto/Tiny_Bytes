using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class BinaryLesson02 : ContentPage
{
    public BinaryLesson02()
    {
        InitializeComponent();
        // The BindingContext is set in the XAML file, so no need to set it here.
    }

    // OnAppearing is still useful to reset the lesson state when the user navigates to the page.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is BinaryLesson02ViewModel viewModel)
        {
            viewModel.InitializeComponent();
        }
    }

    // We no longer need the OnDisappearing, ViewModel_PropertyChanged, 
    // OnNextButtonClicked, or ZeroOneTapSwitch methods.

    // If you haven't bound the Button's Command in XAML, you can keep this:
    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is BinaryLesson02ViewModel viewModel)
        {
            viewModel.NextCommand.Execute(null);
        }
    }
}