using System.Windows.Input;
using Tiny_Bytes_Academy.Views;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Models;
using Tiny_Bytes_Academy.Mocks; // We need this using statement

namespace Tiny_Bytes_Academy.ViewModels;


public class StartPageViewModel : BaseViewModel
{
    private readonly IDataService _dataService;
    private UserModel? _currentUserProfile;

    // Public property to hold the loaded user data
    public UserModel? CurrentUserProfile
    {
        get => _currentUserProfile;
        set => SetProperty(ref _currentUserProfile, value);
    }

    public ICommand GoToMenuPageCommand { get; }
    public ICommand GoToSettingsPageCommand { get; }

    public StartPageViewModel()
    : this(new DesignDataService()) // <-- Pass the safe design-time service
    {
        // The body can be empty because the initialization is handled
        // by the chained call to the main constructor.
    }

    // Service is injected here by the DI container
    public StartPageViewModel(IDataService dataService)
    {
        _dataService = dataService;

        // Start loading the user profile immediately after the service is assigned
        // The property (CurrentUserProfile) will update when the load completes.
        Task.Run(async () => await LoadUserProfile());

        // Command definitions
        GoToMenuPageCommand = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(MenuPage)}"));
        GoToSettingsPageCommand = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(SettingsPage)}"));
    }

    // New method to handle the asynchronous loading logic
    private async Task LoadUserProfile()
    {
        try
        {
            // Call the service and assign the result to the bindable property
            CurrentUserProfile = await _dataService.LoadUserProfileAsync();

            // Optional: If you need to save the initial state after loading the default one:
            // if (CurrentUserProfile.UserName == null)
            // {
            //     await _dataService.SaveUserProfileAsync(CurrentUserProfile);
            // }
        }
        catch (Exception ex)
        {
            // Handle exceptions during file loading (e.g., log them)
            // Console.WriteLine($"Error loading user profile: {ex.Message}");
        }
    }
}