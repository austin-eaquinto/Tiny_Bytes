using System;
using System.ComponentModel;
using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
		BindingContext = new SettingsViewModel();
	}
}