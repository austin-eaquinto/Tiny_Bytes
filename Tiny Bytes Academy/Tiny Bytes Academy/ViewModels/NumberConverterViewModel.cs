using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Threading.Tasks;

namespace TinyBytesAcademy.ViewModels
{
    // A simple implementation of ICommand to handle UI interactions in MVVM
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);

        // Call this method when the CanExecute state changes
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    // --- ViewModel: Handles all the state and conversion logic ---
    public class NumberConverterViewModel : INotifyPropertyChanged
    {
        // Interface requirement for MAUI Data Binding
        public event PropertyChangedEventHandler PropertyChanged;

        // Helper to notify the UI when a property changes
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            // Trigger re-calculation whenever a relevant property changes
            if (propertyName == nameof(InputValue) || propertyName == nameof(InputBase) || propertyName == nameof(OutputBase))
            {
                CalculateConversion();
            }
        }

        // --- Model Data Structures ---
        public class BaseOption
        {
            public int Value { get; set; }
            public string Label { get; set; }
        }

        public List<BaseOption> BaseOptions { get; } = new List<BaseOption>
        {
            new BaseOption { Value = 10, Label = "Decimal" },
            new BaseOption { Value = 2, Label = "Binary" },
            new BaseOption { Value = 16, Label = "Hexadecimal" }
        };

        // --- Private Backing Fields ---
        private string _inputValue = string.Empty;
        private int _inputBase = 10;
        private int _outputBase = 2;
        private string _convertedResult = "Enter a value above.";
        private string _conversionError = null;
        private string _copyMessage = null;

        // --- Observable Properties (Bound to XAML View) ---

        public string InputValue
        {
            get => _inputValue;
            set
            {
                if (_inputValue != value)
                {
                    _inputValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public int InputBase
        {
            get => _inputBase;
            set
            {
                if (_inputBase != value)
                {
                    _inputBase = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OutputBase
        {
            get => _outputBase;
            set
            {
                if (_outputBase != value)
                {
                    _outputBase = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ConvertedResult
        {
            get => _convertedResult;
            private set
            {
                if (_convertedResult != value)
                {
                    _convertedResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ConversionError
        {
            get => _conversionError;
            private set
            {
                if (_conversionError != value)
                {
                    _conversionError = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsErrorVisible)); // Notify visibility change
                }
            }
        }

        public bool IsErrorVisible => !string.IsNullOrEmpty(ConversionError);

        public string CopyMessage
        {
            get => _copyMessage;
            private set
            {
                if (_copyMessage != value)
                {
                    _copyMessage = value;
                    OnPropertyChanged();
                    // Optionally notify a UI visibility property for the message pop-up
                }
            }
        }

        // --- Commands ---

        public ICommand SetInputBaseCommand { get; }
        public ICommand SetOutputBaseCommand { get; }
        public ICommand CopyToClipboardCommand { get; }

        public NumberConverterViewModel()
        {
            // Commands for setting the base from the UI buttons/radios
            SetInputBaseCommand = new RelayCommand(p => InputBase = (int)p);
            SetOutputBaseCommand = new RelayCommand(p => OutputBase = (int)p);

            // Command for clipboard functionality
            CopyToClipboardCommand = new RelayCommand(async p => await CopyToClipboardAsync());

            // Initial calculation
            CalculateConversion();
        }

        // --- Core Base Conversion Logic ---

        private void CalculateConversion()
        {
            string inputVal = InputValue.Trim();
            int inBase = InputBase;
            int outBase = OutputBase;

            ConversionError = null; // Clear previous error

            if (string.IsNullOrWhiteSpace(inputVal))
            {
                ConvertedResult = "Enter a value above.";
                return;
            }

            // 1. Validate input characters based on selected base
            string pattern = inBase switch
            {
                2 => "^[01]+$",
                10 => "^\\d+$",
                16 => "^[0-9a-fA-F]+$",
                _ => ".*" // Should not happen
            };

            if (!Regex.IsMatch(inputVal, pattern))
            {
                ConversionError = $"Input contains characters invalid for Base {inBase}.";
                ConvertedResult = "INVALID INPUT";
                return;
            }

            // 2. Parse input string into a BigInteger (handles arbitrarily large numbers)
            BigInteger decimalValue;
            try
            {
                // The simplest, standard MAUI way for fixed bases is to use a custom function 
                // leveraging the base-specific parsing rules.

                decimalValue = 0;
                BigInteger power = 1;
                string upperInput = inputVal.ToUpper();

                for (int i = upperInput.Length - 1; i >= 0; i--)
                {
                    int digit;
                    char c = upperInput[i];

                    if (c >= '0' && c <= '9')
                    {
                        digit = c - '0';
                    }
                    else if (c >= 'A' && c <= 'F' && inBase == 16)
                    {
                        digit = c - 'A' + 10;
                    }
                    else
                    {
                        // Should have been caught by regex, but as a safeguard:
                        ConversionError = $"Input is not a valid number in Base {inBase}.";
                        ConvertedResult = "INVALID INPUT";
                        return;
                    }

                    if (digit >= inBase || digit < 0)
                    {
                        // Should have been caught by regex, but as a safeguard:
                        ConversionError = $"Input is not a valid number in Base {inBase}.";
                        ConvertedResult = "INVALID INPUT";
                        return;
                    }

                    decimalValue += digit * power;
                    power *= inBase;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Conversion Error: {ex.Message}");
                ConversionError = $"A conversion error occurred: {ex.Message}";
                ConvertedResult = "ERROR";
                return;
            }


            // 3. Convert the decimal BigInteger to the target base string
            try
            {
                string result;
                if (outBase == 10)
                {
                    result = decimalValue.ToString();
                }
                else if (outBase == 2)
                {
                    result = BigIntegerToBinary(decimalValue);
                }
                else // outBase == 16
                {
                    result = BigIntegerToHex(decimalValue); // Uses the new, fixed helper
                }

                // 4. Format the result
                ConvertedResult = result.ToUpper();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Output Error: {ex.Message}");
                ConversionError = $"A result formatting error occurred: {ex.Message}";
                ConvertedResult = "ERROR";
            }
        }

        // Helper function to convert BigInteger to Binary string
        private string BigIntegerToBinary(BigInteger value)
        {
            if (value == 0) return "0";
            string binary = "";
            BigInteger temp = value;
            while (temp > 0)
            {
                binary = (temp % 2 == 0 ? "0" : "1") + binary;
                temp /= 2;
            }
            return binary;
        }

        /**
         * FIX: Explicitly cleans up the BigInteger Hex string to remove unnecessary
         * leading zeros that C# formatting sometimes adds to pad to an even byte count.
         */
        private string BigIntegerToHex(BigInteger value)
        {
            if (value.IsZero) return "0";

            // Use the standard hex format specifier
            string hex = value.ToString("X");

            // Trim leading zeros, unless the number is zero itself (handled above)
            // Example: "08" becomes "8", "0A" becomes "A", but "00AA" becomes "AA"
            return hex.TrimStart('0');
        }

        // --- MAUI Service Interaction (Clipboard) ---
        private async Task CopyToClipboardAsync()
        {
            if (ConversionError != null || ConvertedResult == "Enter a value above." || ConvertedResult == "INVALID INPUT" || ConvertedResult == "ERROR")
            {
                CopyMessage = "Cannot copy error or empty state.";
            }
            else
            {
                try
                {
                    // This uses the MAUI Clipboard Service
                    await Clipboard.SetTextAsync(ConvertedResult);
                    CopyMessage = "Copied!";
                }
                catch (Exception)
                {
                    CopyMessage = "Copy failed.";
                }
            }

            // Clear the message after a delay
            await Task.Delay(1500);
            CopyMessage = null;
        }
    }
}