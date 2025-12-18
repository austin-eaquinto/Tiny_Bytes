using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Messages;
using Tiny_Bytes_Academy.Models;
using Tiny_Bytes_Academy.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tiny_Bytes_Academy.ViewModels
{
    // Ensure the class is public and has a public parameterless constructor
    public class BinaryLesson01ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public bool IsClickCounterStep1Visible => _currentIndex == 1;
        public bool IsClickCounterStep3Visible => _currentIndex == 3;
        public bool IsClickCounterStep4Visible => _currentIndex == 4;
        public bool _areAnswersCorrect = false;
        public bool AreAnswersCorrect
        {
            get => _areAnswersCorrect;
            set { if (_areAnswersCorrect != value)
                {
                    _areAnswersCorrect = value;
                    ((Command)NextCommand).ChangeCanExecute();
                }    
            }
        }
        private bool CanGoNext()
        {
            if (_currentIndex != 4)
            {
                return true;
            }

            return _areAnswersCorrect;
        }

        // For future user data persistence
        private readonly IDataService _dataService;
        private readonly UserModel _currentUserProfile;

        private List<BinLesson1Step> _steps;
        private int _currentIndex; // To track the current step index
        private string _currentInstruction; // To hold the current instruction text
        public string LessonTitle { get; } = "Lesson 1";
        public string CurrentInstruction
        {
            get => _currentInstruction;
            set { _currentInstruction = value; OnPropertyChanged(); }
        }
        // New read-only property that indicates whether the lightbulb container should be visible.
        // (rebuild)

        public string NextButtonText => _currentIndex < _steps.Count - 1 ? "Next" : "Finish";

        public ICommand NextCommand { get; }
        //public ICommand HintCommand { get; }
        public BinaryLesson01ViewModel(IDataService dataService, UserModel userProfile)
        {
            _dataService = dataService;
            _currentUserProfile = userProfile;

            InitializeSteps();

            NextCommand = new Command(async () => await OnNext(), CanGoNext);
            //HintCommand = new Command(async (obj) => await OnHint(), (obj) => IsHintVisible);

            InitializeComponent();  // set initial instruction
        }

        private void InitializeSteps()
        {
            _steps = new List<BinLesson1Step>
            {
                new BinLesson1Step("In decimal, when you go past 9, you add another digit" +
                " (10, 11, …). In binary, you go past 1 much faster, so you add digits more" +
                " often."),
                new BinLesson1Step("For example, notice how adding “1” to “9” makes the number" +
                " “10” by turning “9” into “0” and placing “1” to the left of the “0”. This" +
                " repeats when “1” is added to 99, 999, and so on."),
                new BinLesson1Step("This logic is the same in binary but happens much" +
                " more frequently because only 0 and 1 are used. That’s a small number range!"),
                new BinLesson1Step("Starting with the number zero count up to eight." +
                " At \"8\" a digit has been added to the left three times. Whether this" +
                " seems complicated or simple to you, it is important to understand for later" +
                " concepts."),
                new BinLesson1Step("Practice: Convert numbers 0-7 into binary.")
            };
        }

        public void InitializeComponent()   // reset the lesson
        {
            _currentIndex = 0;
            CurrentInstruction = _steps[_currentIndex].Content;
            OnPropertyChanged(nameof(NextButtonText));
            // notify UI about visibility change for the lightbulb and button

        }

        private async Task OnNext()
        {
            if (_currentIndex < _steps.Count - 1)
            {
                _currentIndex++;
                CurrentInstruction = _steps[_currentIndex].Content; // update instruction
                OnPropertyChanged(nameof(NextButtonText));
                OnPropertyChanged(nameof(IsClickCounterStep1Visible));
                OnPropertyChanged(nameof(IsClickCounterStep3Visible));
                OnPropertyChanged(nameof(IsClickCounterStep4Visible));
            }
            else
            {
                // Finished lesson 1 - send session-only completion message
                WeakReferenceMessenger.Default.Send(new LessonCompletedMessage(1));

                // finished - navigate to the next lesson/page
                await Shell.Current.GoToAsync("///MenuPage");
            }

            _areAnswersCorrect = false;
            ((Command)NextCommand).ChangeCanExecute();
        }
    }

    public class BinLesson1Step
    {
        public string Content { get; set; }
        public BinLesson1Step(string content) => Content = content;
    }

}
