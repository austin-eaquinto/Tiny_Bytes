using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class HexLesson01 : ContentPage
{
    public HexLesson01(HexLesson01ViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (this.BindingContext is HexLesson01ViewModel viewModel)
        {
            viewModel.InitializeComponent();
        }
    }
}