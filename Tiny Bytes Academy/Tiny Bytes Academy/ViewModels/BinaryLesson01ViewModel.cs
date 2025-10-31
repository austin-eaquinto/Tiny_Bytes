using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Tiny_Bytes_Academy.Views;

namespace Tiny_Bytes_Academy.ViewModels
{
    public class BinaryLesson01ViewModel : BaseViewModel
    {
        private readonly List<BinLesson1Step> _steps;
        private int _currentIndex; // To track the current step index
        private string _currentInstruction; // To hold the current instruction text

        public string LessonTitle { get; } = "Lesson 1";

        public string CurrentInstruction
        {
            get => _currentInstruction;
            set { _currentInstruction = value; OnPropertyChanged(); }
        }

        // New read-only property that indicates whether the lightbulb container should be visible.
        public bool IsLightbulbVisible => _currentIndex == 1;

        public string NextButtonText => _currentIndex < _steps.Count - 1 ? "Next" : "Finish";

        public ICommand NextCommand { get; }

        public BinaryLesson01ViewModel()
        {
            _steps = new List<BinLesson1Step>
            {
                new BinLesson1Step("The Basics. The entire binary number system is made up of zeroes and ones. Each digit, whether it be a '0' or a '1' is " +
                "called a bit."),
                new BinLesson1Step("Each bit can only be in one state, meaning 0 or 1. Another way to put it is that it can be ON (1), or OFF (0). Flip the " +
                "switch to toggle the light."),
                new BinLesson1Step("Binary is a base-2 number system. What does that mean? Think of it this way."),
                new BinLesson1Step("The decimal system uses numbers ten numbers: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9. We keep repeating these " +
                "numbers over and over to create bigger numbers like 10, 11, 12 or 100, 101, 102 and so on. Another name for the decimal system is base-10."),
                new BinLesson1Step("Binary only uses two numbers: 0 and 1. How do we count in base-2? The same way as base-10."),
                new BinLesson1Step("Look at this: In base-10 we count 0, 1, 2, 3 and in base-2 we count 0, 1, 10, 11. See the pattern? It's okay if you don't," +
                "we're going to practice it."),
                new BinLesson1Step("Each digit is called a 'bit'."),
                new BinLesson1Step("Congratulations! Lesson complete.")
            };

            NextCommand = new Command(async () => await OnNext());

            InitializeComponent();  // set initial instruction
        }
        public void InitializeComponent()   // reset the lesson
        {
            _currentIndex = 0;
            CurrentInstruction = _steps[_currentIndex].Content;
            OnPropertyChanged(nameof(NextButtonText));
            // notify UI about visibility change for the lightbulb and button
            OnPropertyChanged(nameof(IsLightbulbVisible));
        }

        private async Task OnNext()
        {
            if (_currentIndex < _steps.Count - 1)
            {
                _currentIndex++;
                CurrentInstruction = _steps[_currentIndex].Content; // update instruction
                OnPropertyChanged(nameof(NextButtonText));
                // notify UI about visibility change
                OnPropertyChanged(nameof(IsLightbulbVisible));
            }
            else
            {
                // finished - navigate to the next lesson/page
                await Shell.Current.GoToAsync("///BinaryLesson02");
            }
        }
    }

    public class BinLesson1Step
    {
        public string Content { get; set; }
        public BinLesson1Step(string content) => Content = content;
    }

}
