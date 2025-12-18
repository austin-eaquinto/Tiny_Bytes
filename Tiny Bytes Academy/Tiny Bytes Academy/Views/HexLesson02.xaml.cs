using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class HexLesson02 : ContentPage
{
    public HexLesson02(HexLesson02ViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (this.BindingContext is HexLesson02ViewModel viewModel)
        {
            viewModel.InitializeComponent();
        }
    }
}