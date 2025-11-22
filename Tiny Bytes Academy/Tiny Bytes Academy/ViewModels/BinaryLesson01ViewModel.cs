using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Models;
using Tiny_Bytes_Academy.Views;

namespace Tiny_Bytes_Academy.ViewModels
{
    // Ensure the class is public and has a public parameterless constructor
    public class BinaryLesson01ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private readonly UserModel _currentUserProfile;
        private List<BinLesson1Step> _steps;
        private int _currentIndex; // To track the current step index
        private string _currentInstruction; // To hold the current instruction text
        public string LessonTitle { get; } = " Binary Lesson 1";
        public string CurrentInstruction
        {
            get => _currentInstruction;
            set { _currentInstruction = value; OnPropertyChanged(); }
        }
        // New read-only property that indicates whether the lightbulb container should be visible.
        public bool IsLightbulbVisible => _currentIndex == 1;
        public string NextButtonText => _currentIndex < _steps.Count - 1 ? "Next" : "Finish";
        public ICommand NextCommand { get; }

        public BinaryLesson01ViewModel(IDataService dataService, UserModel userProfile)
        {
            _dataService = dataService;
            _currentUserProfile = userProfile;

            InitializeSteps();

            NextCommand = new Command(async () => await OnNext());

            InitializeComponent();  // set initial instruction
        }

        private void InitializeSteps()
        {
            _steps = new List<BinLesson1Step>
            {
                new BinLesson1Step("Have you ever watched a show, explored your pc, or looked up videos and seen something like this? \"01100101011000001010101111101011\"." +
                " That's binary. Another name for it is machine code. Believe it or not this is code and this is what every single computer uses to accomplish everything" +
                " it can do whether it be PC, phone, smartwatch, microwave, refrigerator or a battery powered toy. Let's learn about it!"),
                new BinLesson1Step("The entire binary number system is made up of zeroes and ones. Each digit, whether it be a '0' or a '1' is" +
                " called a bit. Each zero and one means something. Turn this light on and off (lightbulb animation). In binary, 0 = Off and 1 = On."),
                new BinLesson1Step("Go ahead and turn the light on and off again. Besides Off = 0 and On = 1, what do you notice? (Lightbulb and single bit animations." +
                " Toggling one toggles the other.) Well, one way to put it is this- When the light is on, electricity is running through the wire and when it's off the" +
                " electricity has stopped flowing. This is important to know for later."),
                new BinLesson1Step("We now know that a bit holds a signal On(HIGH), which is represented by a \"1\", or Off(LOW), which is represented by a \"0\". That's" +
                " great but what does this mean in a string of bits like \"0100011101101111\"?"),
                new BinLesson1Step("To help computers and us understand bits a little better we can group them into bytes. Eight bits equals one byte. Below is a single" +
                " byte. Click the numbers to get an idea for all the possible combinations of bits in a byte. (byte animation)"),
                new BinLesson1Step("Congratulations! Lesson complete.")
            };
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
            {   // update the model
                // mark the specific lesson as complete on the shared, live user profile
                _currentUserProfile.IsBinary01Complete = true;
                // persist the changes
                // Call the IDataService to serialize and save the updated user profile to the JSON
                await _dataService.SaveUserProfileAsync(_currentUserProfile);
                // finished - navigate to the next lesson/page
                await Shell.Current.GoToAsync("///MenuPage");
            }
        }
    }

    public class BinLesson1Step
    {
        public string Content { get; set; }
        public BinLesson1Step(string content) => Content = content;
    }

}
