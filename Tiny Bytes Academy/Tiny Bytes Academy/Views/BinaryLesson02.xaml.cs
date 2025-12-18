using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class BinaryLesson02 : ContentPage
{
    public BinaryLesson02(BinaryLesson02ViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (this.BindingContext is BinaryLesson02ViewModel viewModel)
        {
            // This resets the lesson and sets up the bits correctly
            viewModel.InitializeComponent();
        }
    }
}