using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Messages;
using Tiny_Bytes_Academy.Models;

namespace Tiny_Bytes_Academy.ViewModels
{
    public class HexLesson01ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private readonly UserModel _currentUserProfile;
        private List<HexLesson1Step> _steps;
        private int _currentIndex;
        private string _currentInstruction;

        public string LessonTitle { get; } = "Lesson 3";

        public string CurrentInstruction
        {
            get => _currentInstruction;
            set { _currentInstruction = value; OnPropertyChanged(); }
        }

        public string NextButtonText => _currentIndex < _steps.Count - 1 ? "Next" : "Finish";

        // --- VISIBILITY PROPERTIES ---
        // Step 1: Chart (0-15 vs 0-F)
        public bool IsChartVisible => _currentIndex == 1;
        // Step 2: Conversion Examples
        public bool IsConversionVisible => _currentIndex == 2;
        // Step 3: Practice Input
        public bool IsPracticeVisible => _currentIndex == 3;
        // Step 4: Color Demo
        public bool IsColorVisible => _currentIndex == 4;


        // --- PRACTICE STEP DATA ---
        // We bind the user's answers here
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }

        // Colors to show feedback (Red=Wrong, Green=Right, Black=Neutral)
        private Color _ans1Color = Colors.Black;
        public Color Ans1Color { get => _ans1Color; set { _ans1Color = value; OnPropertyChanged(); } }

        private Color _ans2Color = Colors.Black;
        public Color Ans2Color { get => _ans2Color; set { _ans2Color = value; OnPropertyChanged(); } }

        private Color _ans3Color = Colors.Black;
        public Color Ans3Color { get => _ans3Color; set { _ans3Color = value; OnPropertyChanged(); } }

        public ICommand NextCommand { get; }
        public ICommand CheckAnswersCommand { get; }

        public HexLesson01ViewModel(IDataService dataService, UserModel userProfile)
        {
            _dataService = dataService;
            _currentUserProfile = userProfile;

            InitializeSteps();

            NextCommand = new Command(async () => await OnNext());
            CheckAnswersCommand = new Command(OnCheckAnswers);

            InitializeComponent();
        }

        public void InitializeSteps()
        {
            _steps = new List<HexLesson1Step>
            {
                new HexLesson1Step("Along with binary, there’s another number system used in computers called hexadecimal (or “hex” for short)."),
                new HexLesson1Step("Hexadecimal is a base-16 number system. That means it uses sixteen symbols: 0-9 and A-F."),
                new HexLesson1Step("Binary strings can get very long. Hex makes them shorter and easier for humans to read."),
                new HexLesson1Step("Practice: Convert these binary nibbles into Hexadecimal."),
                new HexLesson1Step("Hex is everywhere! For example, colors on websites use hex codes for Red, Green, and Blue."),
                new HexLesson1Step("Hexadecimal is like a shortcut for binary. Once you know how to group bits into 4s, you can read hex easily.")
            };
        }

        public void InitializeComponent()
        {
            _currentIndex = 0;
            CurrentInstruction = _steps[_currentIndex].Content;

            // Clear previous answers
            Answer1 = ""; Answer2 = ""; Answer3 = "";
            Ans1Color = Colors.Black; Ans2Color = Colors.Black; Ans3Color = Colors.Black;
            OnPropertyChanged(nameof(Answer1));
            OnPropertyChanged(nameof(Answer2));
            OnPropertyChanged(nameof(Answer3));

            RefreshVisibility();
        }

        private void OnCheckAnswers()
        {
            // Logic: 0001 = 1, 1100 = C, 1111 = F
            Ans1Color = (Answer1?.Trim() == "1") ? Colors.Green : Colors.Red;
            Ans2Color = (Answer2?.Trim().ToUpper() == "C") ? Colors.Green : Colors.Red;
            Ans3Color = (Answer3?.Trim().ToUpper() == "F") ? Colors.Green : Colors.Red;
        }

        private void RefreshVisibility()
        {
            OnPropertyChanged(nameof(NextButtonText));
            OnPropertyChanged(nameof(IsChartVisible));
            OnPropertyChanged(nameof(IsConversionVisible));
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
                WeakReferenceMessenger.Default.Send(new LessonCompletedMessage(3)); // Lesson 3 corresponds to Hex 1
                await Shell.Current.GoToAsync("///MenuPage");
            }
        }
    }

    public class HexLesson1Step
    {
        public string Content { get; set; }
        public HexLesson1Step(string content) => Content = content;
    }
}