using System.ComponentModel;
using Tiny_Bytes_Academy.ViewModels;

namespace Tiny_Bytes_Academy.Views;

public partial class BinaryLesson01 : ContentPage
{
    private int _clickCountStep1 = 9;
    private int _clickCountStep3 = 0;

    public BinaryLesson01(BinaryLesson01ViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        // 1. "Juice": Scale up the number to draw attention
        await CounterLabel.ScaleTo(1.5, 100);

        // 2. Logic: Toggle between 9 and 10 for the lesson demo
        if (_clickCountStep1 == 9)
        {
            _clickCountStep1 = 10;
            CounterButton.Text = "-";
        }
        else
        {
            _clickCountStep1 = 9; // Reset so they can try again
            CounterButton.Text = "+";
        }

        // 3. Update the text
        CounterLabel.Text = _clickCountStep1.ToString();

        // 4. Scale back down
        await CounterLabel.ScaleTo(1.0, 100);
    }


    private async void OnBinaryCounterClicked(object sender, EventArgs e)
    {
        // 1. Calculate what the next number will be
        int nextValue = _clickCountStep3 + 1;
        if (nextValue > 8) nextValue = 0;

        // 2. Get the binary string lengths to compare
        // Convert.ToString(value, 2) turns the int into binary (e.g., 2 -> "10")
        int currentLength = Convert.ToString(_clickCountStep3, 2).Length;
        int nextLength = Convert.ToString(nextValue, 2).Length;

        // Check if we are "adding a digit" (e.g., 1 digit -> 2 digits)
        bool shouldAnimate = nextLength > currentLength;

        // 3. Conditional Animation: Scale UP
        if (shouldAnimate)
        {
            await Task.WhenAll(
                //DecimalCountLabel.ScaleTo(1.5, 100), // I don't want this to animate for now
                BinaryCountLabel.ScaleTo(1.5, 100)
            );
        }

        // 4. Update the Data
        _clickCountStep3 = nextValue;
        DecimalCountLabel.Text = _clickCountStep3.ToString();
        BinaryCountLabel.Text = Convert.ToString(_clickCountStep3, 2);

        // 5. Conditional Animation: Scale DOWN
        if (shouldAnimate)
        {
            await Task.WhenAll(
                DecimalCountLabel.ScaleTo(1.0, 100),
                BinaryCountLabel.ScaleTo(1.0, 100)
            );
        }
    }

    // the function CheckStep4Answers will check if the answers in step 4 are correct
    private void OnCheckStep4AnswersClicked(object sender, TextChangedEventArgs e)
    {
        bool isCorrect = false;
        bool allCorrect = false;

        if (sender is Entry entry)
        {
            if (entry == Number4 && entry.Text == "4") isCorrect = true;
            else if (entry == Number5 && entry.Text == "5") isCorrect = true;
            else if (entry == Number6 && entry.Text == "6") isCorrect = true;
            else if (entry == Number7 && entry.Text == "7") isCorrect = true;


            allCorrect = Number4.Text == "4" &&
                         Number5.Text == "5" &&
                         Number6.Text == "6" &&
                         Number7.Text == "7";

            if (BindingContext is BinaryLesson01ViewModel viewModel)
            {
                // update VM state
                viewModel.AreAnswersCorrect = allCorrect;

                // Ensure the Next/Finish button reflects the VM's CanExecute state immediately.
                // This makes the Finish button usable even if the VM doesn't raise PropertyChanged.
                NextButton.IsEnabled = viewModel.NextCommand?.CanExecute(null) ?? true;
            }

            if (string.IsNullOrEmpty(entry.Text))
            {
                entry.TextColor = Colors.Black; // default color
            }
            else if (isCorrect)
            {
                entry.TextColor = Colors.Green;
            }
            else
            {
                entry.TextColor = Colors.Red;
            }
        }
    }

    protected override void OnAppearing() // reset the lesson when the page appears
    {
        base.OnAppearing();
        if (this.BindingContext is BinaryLesson01ViewModel viewModel)
        {
            viewModel.InitializeComponent();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;

            // Ensure the Next/Finish button reflects the VM's CanExecute state initially.
            // This prevents finishing the lesson until answers are correct on the final step.
            NextButton.IsEnabled = viewModel.NextCommand?.CanExecute(null) ?? true;
        }
    }

    // this function
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
        if (BindingContext is BinaryLesson01ViewModel vm)
        {
            // Update the Next button enabled state whenever the VM notifies changes
            // (e.g., AreAnswersCorrect or when moving to the final step).
            NextButton.IsEnabled = vm.NextCommand?.CanExecute(null) ?? true;
        }
    }

    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        // Only invoke the VM command when it can execute. This ensures the final "Finish"
        // action is blocked until the VM allows it (e.g., after answers are correct).
        if (BindingContext is BinaryLesson01ViewModel viewModel &&
            viewModel.NextCommand != null &&
            viewModel.NextCommand.CanExecute(null))
        {
            viewModel.NextCommand.Execute(null);
        }
    }
}