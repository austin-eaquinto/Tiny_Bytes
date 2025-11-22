using System.ComponentModel;
using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class BinaryLesson01 : ContentPage
{
    bool isLightOn = false;

    public BinaryLesson01(BinaryLesson01ViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing() // reset the lesson when the page appears
    {
        base.OnAppearing();
        if (this.BindingContext is BinaryLesson01ViewModel viewModel)
        {
            viewModel.InitializeComponent();

            // ensure initial visibility matches the VM
            LightbulbContainer.IsVisible = viewModel.IsLightbulbVisible;

            // subscribe so the view updates whenever the VM's IsLightbulbVisible changes
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (this.BindingContext is BinaryLesson01ViewModel viewModel)
        {
            viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(BinaryLesson01ViewModel.IsLightbulbVisible) || string.IsNullOrEmpty(e.PropertyName))
        {
            if (BindingContext is BinaryLesson01ViewModel vm)
            {
                LightbulbContainer.IsVisible = vm.IsLightbulbVisible;
            }
        }
    }

    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        // Just invoke the VM command; VM will raise PropertyChanged for IsLightbulbVisible and the handler will update the UI.
        if (BindingContext is BinaryLesson01ViewModel viewModel)
        {
            viewModel.NextCommand.Execute(null);
        }
    }

    private void OnToggleButtonClicked(object sender, EventArgs e)
    {
        isLightOn = !isLightOn; // Toggle the state

        if (isLightOn)
        {
            LightbulbImage.Source = "lightbulb_on.png"; // Change to "on" image
            ToggleButton.Text = "On";      // Change button text
            ToggleButton.BackgroundColor = Color.FromArgb("#2ECC71");
        }
        else
        {
            LightbulbImage.Source = "lightbulb_off.png"; // Change to "off" image
            ToggleButton.Text = "Off";       // Change button text
            ToggleButton.BackgroundColor = Color.FromArgb("#E74C3C");
        }
    }
}