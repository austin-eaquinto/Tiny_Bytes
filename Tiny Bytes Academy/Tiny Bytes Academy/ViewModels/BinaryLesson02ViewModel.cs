using System.Collections.Generic;
using System.Windows.Input;

namespace Tiny_Bytes_Academy.ViewModels
{
    class BinaryLesson02ViewModel : BaseViewModel
    {
        private readonly List<BinLesson2Step> _steps;
        private int _currentIndex;
        private string _currentInstruction;

        public string LessonTitle { get; } = "Lesson 2";

        public string CurrentInstruction
        {
            get => _currentInstruction;
            set { _currentInstruction = value; OnPropertyChanged(); }
        }

        public string NextButtonText => _currentIndex < _steps.Count - 1 ? "Next" : "Finish";

        public ICommand NextCommand { get; }

        public BinaryLesson02ViewModel()
        {
            _steps = new List<BinLesson2Step>
            {
                new BinLesson2Step("Binary is a base-2 number system."),
                new BinLesson2Step("It only uses two symbols: 0 and 1."),
                new BinLesson2Step("Each digit is called a 'bit'."),
                new BinLesson2Step("Congratulations! Lesson complete.")
            };

            _currentIndex = 0;
            CurrentInstruction = _steps[_currentIndex].Content;
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            if (_currentIndex < _steps.Count - 1)
            {
                _currentIndex++;
                CurrentInstruction = _steps[_currentIndex].Content;
                OnPropertyChanged(nameof(NextButtonText));
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
