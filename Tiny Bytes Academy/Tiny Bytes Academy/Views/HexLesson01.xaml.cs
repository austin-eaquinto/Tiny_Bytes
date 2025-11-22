using System;
using System.ComponentModel;
using Tiny_Bytes_Academy.ViewModels;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Models;

namespace Tiny_Bytes_Academy.Views;

public partial class HexLesson01 : ContentPage
{

    public HexLesson01(HexLesson01ViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
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
}