using CommunityToolkit.Mvvm.ComponentModel;

namespace Tiny_Bytes_Academy.Models;

// Model representing information about a lesson in the Tiny Bytes Academy app
public class LessonInfo : ObservableObject // Inheriting from ObservableObject to support property change notifications
{
    public int LessonId { get; set; } // Unique identifier for the lesson

    public string Title { get; set; } // Text displayed on the menu button/item

    public string Route { get; set; } // The Shell Route string used for navigation (e.g., "BinaryLesson01")

    private bool _isCompleted; // Tracks if the user has finished the lesson (used to unlock the next)
    public bool IsCompleted // Indicates if the lesson has been completed
    {
        get => _isCompleted;
        set => SetProperty(ref _isCompleted, value); // Using SetProperty to notify changes
    }

    private bool _isLocked; // Tracks if the lesson button is disabled/greyed out
    public bool IsLocked
    {
        get => _isLocked;
        set => SetProperty(ref _isLocked, value); // Using SetProperty to notify changes
    }
}