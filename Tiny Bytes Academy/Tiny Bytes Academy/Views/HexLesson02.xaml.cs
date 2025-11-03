using System;
using System.ComponentModel;
using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class HexLesson02 : ContentPage
{
    bool isLightOn = false;

    public HexLesson02()
    {
        InitializeComponent();
        BindingContext = new HexLesson02ViewModel();
    }

    protected override void OnAppearing() // reset the lesson when the page appears
    {
        base.OnAppearing();
        if (this.BindingContext is HexLesson02ViewModel viewModel)
        {
            viewModel.InitializeComponent();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (this.BindingContext is HexLesson02ViewModel viewModel)
        {
            viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(HexLesson02ViewModel.IsLightbulbVisible) || string.IsNullOrEmpty(e.PropertyName))
        {
            if (BindingContext is HexLesson02ViewModel vm)
            {
                //LightbulbContainer.IsVisible = vm.IsLightbulbVisible;
            }
        }
    }

    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        if (this.BindingContext is HexLesson02ViewModel viewModel)
        {
            viewModel.NextCommand.Execute(null);
        }
    }

    private void OnToggleButtonClicked(object sender, EventArgs e)
    {
        isLightOn = !isLightOn;
        if (isLightOn)
        {
            //LightbulbImage.Source = "lightbulb_on.png";
        }
        else
        {
            //LightbulbImage.Source = "lightbulb_off.png";
        }
    }
}