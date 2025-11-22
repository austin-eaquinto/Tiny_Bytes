using Tiny_Bytes_Academy.Interfaces;

namespace Tiny_Bytes_Academy.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string route)
    {
        // This is the *only* place in your application outside of AppShell
        // where you directly use the MAUI-specific navigation (Shell.Current).
        await Shell.Current.GoToAsync($"///{route}");
    }
}