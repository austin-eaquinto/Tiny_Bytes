using System.Collections.Generic;
using System.Collections.ObjectModel; // Using ObservableCollection for UI updates
using System.ComponentModel;
using System.Linq; // For .ToList()
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Tiny_Bytes_Academy.ViewModels
{
    // NEW: A class to represent a single bit's state
    public class BitViewModel : INotifyPropertyChanged
    {
        private bool _isZero = true;
        public bool IsZero
        {
            get => _isZero;
            set
            {
                if (_isZero != value)
                {
                    _isZero = value;
                    OnPropertyChanged(nameof(IsZero));
                    OnPropertyChanged(nameof(BitImageSource)); // Notify that the image source depends on this
                }
            }
        }

        public string BitImageSource => IsZero ? "zero_icon.png" : "one_icon.png"; // this is a ternary operator

        public ICommand ToggleCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    class BinaryLesson02ViewModel : BaseViewModel
    {
        private readonly List<BinLesson2Step> _steps;
        private int _currentIndex;
        private string _currentInstruction;

        // NEW: Collection to hold the 8 bits
        public ObservableCollection<BitViewModel> Bits { get; } = new ObservableCollection<BitViewModel>();

        public string LessonTitle { get; } = "Lesson 2";

        public string CurrentInstruction
        {
            get => _currentInstruction;
            set { _currentInstruction = value; OnPropertyChanged(); }
        }

        // This property now controls the visibility of the entire byte
        public bool IsBitVisible => _currentIndex == 1;

        public string NextButtonText => _currentIndex < _steps.Count - 1 ? "Next" : "Finish";

        public ICommand NextCommand { get; }

        public BinaryLesson02ViewModel() // this is the constructor
        {
            _steps = new List<BinLesson2Step>
            {
                new BinLesson2Step("Binary is a base-2 number system."),
                new BinLesson2Step("It only uses two symbols: 0 and 1. Here is a byte:"),
                new BinLesson2Step("Each digit is called a 'bit'."),
                new BinLesson2Step("Congratulations! Lesson complete.")
            };

            _currentIndex = 0;
            CurrentInstruction = _steps[_currentIndex].Content;
            NextCommand = new Command(OnNext);

            // NEW: Initialize the 8 bits
            for (int i = 0; i < 8; i++)
            {
                var bit = new BitViewModel();
                // Command is set here so it can toggle its own state
                bit.ToggleCommand = new Command(() => bit.IsZero = !bit.IsZero);
                Bits.Add(bit);
            }
        }

        public void InitializeComponent()
        {
            _currentIndex = 0;
            CurrentInstruction = _steps[_currentIndex].Content;
            OnPropertyChanged(nameof(NextButtonText));
            OnPropertyChanged(nameof(IsBitVisible));
        }

        private void OnNext()
        {
            if (_currentIndex < _steps.Count - 1)
            {
                _currentIndex++;
                CurrentInstruction = _steps[_currentIndex].Content;
                OnPropertyChanged(nameof(NextButtonText));
                // ADDED: This is crucial to make the byte appear/disappear at the right step
                OnPropertyChanged(nameof(IsBitVisible));
            }
            else
            {
                // finished - handle navigation if needed
            }
        }
    }

    public class BinLesson2Step
    {
        public string Content { get; set; }
        public BinLesson2Step(string content) => Content = content;
    }
}