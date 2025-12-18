using System.Collections.ObjectModel; // Using ObservableCollection for UI updates
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Models;
using CommunityToolkit.Mvvm.Messaging;
using Tiny_Bytes_Academy.Messages;

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
                    OnPropertyChanged(nameof(LightbulbImageSource)); // <--- Notify this changes too
                    OnPropertyChanged(nameof(StateText));
                    OnPropertyChanged(nameof(StateColor));
                }
            }
        }

        public string BitImageSource => IsZero ? "zero_icon.png" : "one_icon.png"; // this is a ternary operator
        public string LightbulbImageSource => IsZero ? "lightbulb_off.png" : "lightbulb_on.png";
        public ICommand ToggleCommand { get; set; }
        public string StateText => IsZero ? "OFF" : "ON";
        public Color StateColor => IsZero ? Colors.Red : Colors.Green;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool IsOn
        {
            get => !IsZero;
            set => IsZero = !value;
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
        public bool IsLightbulbVisible => _currentIndex == 1;
        public string LessonTitle { get; } = "Lesson 2";
        public string CurrentInstruction
        {
            get => _currentInstruction;
            set { _currentInstruction = value; OnPropertyChanged(); }
        }
        public BitViewModel? SingleBit => Bits.FirstOrDefault();
        public ObservableCollection<BitViewModel> NibbleBits { get; } = new ObservableCollection<BitViewModel>();
        public bool IsBitVisible => _currentIndex == 0;
        public bool IsByteVisible => _currentIndex == 2;
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
                new BinLesson2Step("A single digit in binary, whether it is a “1” or “0”, is" +
                " called a bit."),
                new BinLesson2Step("A bit is a measurement of data. As a rule, 1 means ON and" +
                " 0 means OFF. Bits of data travel along the wires of every computer system," +
                " telling the computer what to do, when to do it and how to do it."),
                new BinLesson2Step("There are two more units of measurement. A nibble, which is" +
                " four bits, and a byte, which is eight bits"),
                new BinLesson2Step("When reading a string of bits like “10010110 00100111" +
                " 10111010 00001111 01111000 10111110” it quickly becomes difficult to understand." +
                " This is where hexadecimal helps because it shortens long binary strings into" +
                " something humans can read more easily.")
            };
        }

        public void InitializeComponent()   // reset the lesson
        {
            _currentIndex = 0;
            CurrentInstruction = _steps[_currentIndex].Content;

            InitializeBits();
            OnPropertyChanged(nameof(NextButtonText));
            // notify the UI about visibility change for the byte
            OnPropertyChanged(nameof(IsByteVisible));
            //OnPropertyChanged(nameof(NibbleBits));
            OnPropertyChanged(nameof(IsBitVisible));
            OnPropertyChanged(nameof(SingleBit));
            OnPropertyChanged(nameof(IsLightbulbVisible));
        }

        // Add this method inside BinaryLesson02ViewModel (below InitializeSteps or near InitializeComponent)
        private void InitializeBits()
        {
            // Setup the Byte (8 bits)
            Bits.Clear();
            for (int i = 0; i < 8; i++)
            {
                var bit = new BitViewModel();
                bit.ToggleCommand = new Command(() => bit.IsZero = !bit.IsZero);
                Bits.Add(bit);
            }

            // Setup the Nibble (4 bits) - Independent instances!
            NibbleBits.Clear();
            for (int i = 0; i < 4; i++)
            {
                var bit = new BitViewModel();
                bit.ToggleCommand = new Command(() => bit.IsZero = !bit.IsZero);
                NibbleBits.Add(bit);
            }
        }

        private async Task OnNext()
        {
            if (_currentIndex < _steps.Count - 1)
            {
                _currentIndex++;
                CurrentInstruction = _steps[_currentIndex].Content;
                OnPropertyChanged(nameof(NextButtonText));
                OnPropertyChanged(nameof(IsBitVisible));
                OnPropertyChanged(nameof(NibbleBits));
                OnPropertyChanged(nameof(IsByteVisible));
                OnPropertyChanged(nameof(SingleBit));
                OnPropertyChanged(nameof(IsLightbulbVisible));

                if (_currentIndex == 0 && Bits.Count == 0)
                {
                    InitializeBits();
                }
            }
            else
            {
                // Do NOT persist completion if the app should reset on close.
                // Notify the menu to mark lesson 2 complete for this session.
                WeakReferenceMessenger.Default.Send(new LessonCompletedMessage(2));

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