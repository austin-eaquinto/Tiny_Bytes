using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Graphics;
using System.ComponentModel;
using System.Windows.Input;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Messages;
using Tiny_Bytes_Academy.Models;

namespace Tiny_Bytes_Academy.ViewModels
{
    public class HexLesson02ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private readonly UserModel _currentUserProfile;
        private List<HexLesson2Step> _steps;
        private int _currentIndex;
        private string _currentInstruction;

        public string LessonTitle { get; } = "Lesson 4";

        public string CurrentInstruction
        {
            get => _currentInstruction;
            set { _currentInstruction = value; OnPropertyChanged(); }
        }

        public string NextButtonText => _currentIndex < _steps.Count - 1 ? "Next" : "Finish";

        // --- VISIBILITY PROPERTIES ---
        // Step 1: Visual mapping (1010 = A, etc)
        public bool IsMappingVisible => _currentIndex == 1;
        // Step 2: Split explanation (1111 0000)
        public bool IsSplitStepVisible => _currentIndex == 2;
        // Step 3: Practice (Input the hex)
        public bool IsPracticeVisible => _currentIndex == 3;
        // Step 4: Color Brick
        public bool IsColorVisible => _currentIndex == 4;

        // --- PRACTICE DATA (Step 3) ---
        public string Prac1 { get; set; }
        public string Prac2 { get; set; }
        public string Prac3 { get; set; }

        private Color _prac1Color = Colors.Black;
        public Color Prac1Color { get => _prac1Color; set { _prac1Color = value; OnPropertyChanged(); } }

        private Color _prac2Color = Colors.Black;
        public Color Prac2Color { get => _prac2Color; set { _prac2Color = value; OnPropertyChanged(); } }

        private Color _prac3Color = Colors.Black;
        public Color Prac3Color { get => _prac3Color; set { _prac3Color = value; OnPropertyChanged(); } }

        // --- COLOR DEMO DATA (Step 4) ---
        private string _hexInput = "FF0000";
        public string HexInput
        {
            get => _hexInput;
            set
            {
                _hexInput = value;
                OnPropertyChanged();
                UpdateColorBrick(); // Update color immediately when user types
            }
        }

        private Color _brickColor = Colors.Red;
        public Color BrickColor
        {
            get => _brickColor;
            set { _brickColor = value; OnPropertyChanged(); }
        }

        public ICommand NextCommand { get; }
        public ICommand CheckPracticeCommand { get; }

        public HexLesson02ViewModel(IDataService dataService, UserModel userProfile)
        {
            _dataService = dataService;
            _currentUserProfile = userProfile;

            InitializeSteps();

            NextCommand = new Command(async () => await OnNext());
            CheckPracticeCommand = new Command(OnCheckPractice);

            InitializeComponent();
        }

        private void InitializeSteps()
        {
            _steps = new List<HexLesson2Step>
            {
                new HexLesson2Step("Binary numbers can get long and tricky to read. Hexadecimal makes them shorter by grouping binary digits into sets of four."),
                new HexLesson2Step("Each group of 4 binary digits (a nibble) maps to one hex digit. For example: 1010 = A and 1100 = C, so 10101100 = AC."),
                new HexLesson2Step("Let’s convert 11110000. \nStep 1: Split into groups (1111 0000). \nStep 2: Convert each (F and 0). \nAnswer: F0."),
                new HexLesson2Step("Practice: Enter the Hex equivalent for these binary groups:"),
                new HexLesson2Step("Hex is used in colors (RGB). The first two digits are Red, the middle are Green, the last are Blue. Try changing the code below!"),
                new HexLesson2Step("Hexadecimal is a shortcut for binary. By grouping bits into 4s, you can quickly translate long strings into something readable.")
            };
        }

        public void InitializeComponent()
        {
            _currentIndex = 0;
            CurrentInstruction = _steps[_currentIndex].Content;

            // Reset Inputs
            Prac1 = ""; Prac2 = ""; Prac3 = "";
            Prac1Color = Colors.Black; Prac2Color = Colors.Black; Prac3Color = Colors.Black;
            OnPropertyChanged(nameof(Prac1)); OnPropertyChanged(nameof(Prac2)); OnPropertyChanged(nameof(Prac3));

            // Reset Color Demo
            HexInput = "FF0000";

            RefreshVisibility();
        }

        private void OnCheckPractice()
        {
            // ANSWERS: 0101 = 5, 1011 = B, 1110 = E
            Prac1Color = (Prac1?.Trim() == "5") ? Colors.Green : Colors.Red;
            Prac2Color = (Prac2?.Trim().ToUpper() == "B") ? Colors.Green : Colors.Red;
            Prac3Color = (Prac3?.Trim().ToUpper() == "E") ? Colors.Green : Colors.Red;
        }

        private void UpdateColorBrick()
        {
            if (string.IsNullOrEmpty(HexInput)) return;

            string hex = HexInput.Trim().Replace("#", "");

            // simple validation to ensure valid hex length
            if (hex.Length == 6)
            {
                try
                {
                    BrickColor = Color.FromArgb("#" + hex);
                }
                catch
                {
                    // invalid hex, ignore or set to default
                }
            }
        }

        private void RefreshVisibility()
        {
            OnPropertyChanged(nameof(NextButtonText));
            OnPropertyChanged(nameof(IsMappingVisible));
            OnPropertyChanged(nameof(IsSplitStepVisible));
            OnPropertyChanged(nameof(IsPracticeVisible));
            OnPropertyChanged(nameof(IsColorVisible));
        }

        private async Task OnNext()
        {
            if (_currentIndex < _steps.Count - 1)
            {
                _currentIndex++;
                CurrentInstruction = _steps[_currentIndex].Content;
                RefreshVisibility();
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new LessonCompletedMessage(4));
                await Shell.Current.GoToAsync("///MenuPage");
            }
        }
    }

    public class HexLesson2Step
    {
        public string Content { get; set; }
        public HexLesson2Step(string content) => Content = content;
    }
}