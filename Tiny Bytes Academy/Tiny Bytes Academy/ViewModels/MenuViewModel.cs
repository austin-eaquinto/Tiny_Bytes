using System.Windows.Input;

namespace Tiny_Bytes_Academy.ViewModels;

public class MenuViewModel : BaseViewModel
{
    private const string V = "Menu";

    public string Message { get; set; } = V;

    public ICommand GotoBinaryLesson1Command { get; }
    public ICommand GotoBinaryLesson2Command { get; }
    public ICommand GotoHexLesson1Command { get; }
    public ICommand GotoHexLesson2Command { get; }
    public ICommand GotoNumberConverterCommand { get; }

    public MenuViewModel()
    {
        GotoBinaryLesson1Command = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(Views.BinaryLesson01)}"));
        GotoBinaryLesson2Command = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(Views.BinaryLesson02)}"));
        GotoHexLesson1Command = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(Views.HexLesson01)}"));
        GotoHexLesson2Command = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(Views.HexLesson02)}"));
        GotoNumberConverterCommand = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(Views.NumberConverterPage)}"));
    }

    private async Task GoToLessonAsync(string lessonRoute)
    {
        await Shell.Current.GoToAsync($"///{lessonRoute}");
        // Use GoToAsync(route) if it's a relative path, or //route for an absolute path.
        // The previous lesson used "///" for an absolute path, which works too.
        // await Shell.Current.GoToAsync($"///{route}");
    }
}
