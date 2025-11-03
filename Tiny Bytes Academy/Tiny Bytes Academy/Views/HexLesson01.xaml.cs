using System;
using System.ComponentModel;
using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class HexLesson01 : ContentPage
{
	bool isLightOn = false;

    public HexLesson01()
	{
		InitializeComponent();
		BindingContext = new HexLesson01ViewModel();
    }

	protected override void OnAppearing() // reset the lesson when the page appears
	{
		base.OnAppearing();
		if (this.BindingContext is HexLesson01ViewModel viewModel)
		{
			viewModel.InitializeComponent();
		}
    }

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		if (this.BindingContext is HexLesson01ViewModel viewModel)
		{
			viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }
    }

	private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == nameof(HexLesson01ViewModel.IsLightbulbVisible) || string.IsNullOrEmpty(e.PropertyName))
		{
			if (BindingContext is HexLesson01ViewModel vm)
			{
				//LightbulbContainer.IsVisible = vm.IsLightbulbVisible;
			}
        }
    }

	private void OnNextButtonClicked(object sender, EventArgs e)
	{
		if(this.BindingContext is HexLesson01ViewModel viewModel)
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