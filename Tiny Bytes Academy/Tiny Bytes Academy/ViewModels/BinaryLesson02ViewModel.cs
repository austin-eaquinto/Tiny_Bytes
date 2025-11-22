using System.Collections.Generic;
using System.Collections.ObjectModel; // Using ObservableCollection for UI updates
using System.ComponentModel;
using System.Linq; // For .ToList()
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Models;

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

    public class BinaryLesson02ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private readonly UserModel _currentUserProfile;
        private List<BinLesson2Step> _steps;
        private int _currentIndex;
        private string _currentInstruction;
        // Collection to hold the 8 bits
        public ObservableCollection<BitViewModel> Bits { get; } = new ObservableCollection<BitViewModel>();
        public string LessonTitle { get; } = "Binary Lesson 2";
        public string CurrentInstruction
        {
            get => _currentInstruction;
            set { _currentInstruction = value; OnPropertyChanged(); }
        }
        // This property now controls the visibility of the entire byte
        public bool IsBitVisible => _currentIndex == 1;
        public string NextButtonText => _currentIndex < _steps.Count - 1 ? "Next" : "Finish";
        public ICommand NextCommand { get; }

        public BinaryLesson02ViewModel(IDataService dataService, UserModel userProfile) // this is the constructor
        {
            _dataService = dataService;
            _currentUserProfile = userProfile;

            InitializeSteps();

            NextCommand = new Command(async () => await OnNext());

            InitializeComponent();  // set initial instruction
        }

        private void InitializeSteps()
        {
            _steps = new List<BinLesson2Step>
            {
                new BinLesson2Step("Binary Basics #2: Binary is a base-2 number system. What does that mean? Think of it this way-> The decimal system uses ten numbers:" +
                " 0, 1, 2, 3, 4, 5, 6, 7, 8, 9. We keep repeating these numbers over and over to create bigger numbers like 10, 11, 12 or 100, 101, 102 and so on." +
                " Another name for the decimal system is base-10 (because it uses ten base numbers)."),
                new BinLesson2Step("Binary only uses two numbers: 0 and 1. How do we count in base-2? The same way as base-10. In decimal we count 0, 1, 2, 3, ... and" +
                " in base-2 we count 0, 1, 10, 11 and so on. See the pattern? It's okay if you don't,\" +\r\n                \" we're going to practice it."),
                new BinLesson2Step("First, image the numbers 0-9 on a ten-point gear. Each number is on the edge of the gear like so, (gear image). Each time the gear" +
                " is incremented by one number we see the next."),
                new BinLesson2Step("Congratulations! Lesson complete.")
            };
        }

        public void InitializeComponent()   // reset the lesson
        {
            _currentIndex = 0;
            CurrentInstruction = _steps[_currentIndex].Content;
            OnPropertyChanged(nameof(NextButtonText));
            // notify the UI about visibility change for the byte
            OnPropertyChanged(nameof(IsBitVisible));
        }

        private async Task OnNext()
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
                // update the model
                // mark the specific lesson as complete on the shared, live user profile
                _currentUserProfile.IsBinary02Complete = true;
                // persist the changes
                // Call the IDataService to serialize and save the updated user profile to the JSON
                await _dataService.SaveUserProfileAsync(_currentUserProfile);
                // finished - navigate to the next lesson/page
                await Shell.Current.GoToAsync("///MenuPage");
            }
        }
    }

    public class BinLesson2Step
    {
        public string Content { get; set; }
        public BinLesson2Step(string content) => Content = content;
    }
}