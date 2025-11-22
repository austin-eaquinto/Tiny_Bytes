using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Models; // Import the LessonInfo model
using Tiny_Bytes_Academy.Views;   // Import the Views namespace

namespace Tiny_Bytes_Academy.ViewModels;

public partial class MenuPageViewModel : BaseViewModel
{
    // The clean MVVM way: Depend on the interface, not the MAUI Shell.
    private readonly INavigationService _navigationService;

    // The observable list that your CollectionView will bind to.
    public ObservableCollection<LessonInfo> Lessons { get; set; } = new();

    public MenuPageViewModel() :
        this(Shell.Current.Handler.MauiContext.Services.GetService<INavigationService>())
    {
    }

    // The constructor uses Dependency Injection to receive the NavigationService.
    public MenuPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        // 1. Load the initial set of lessons
        LoadLessonData();

        // 2. Set the locked/unlocked status
        UpdateLessonLockStatus();
    }

    // Single command to handle all lesson navigation, parameter is the LessonInfo object.
    [RelayCommand]
    private async Task GoToLesson(LessonInfo lesson)
    {
        if (lesson.IsLocked)
        {
            // You can add a prompt or animation here if you want to tell the user it's locked.
            // For now, we'll just return.
            Console.WriteLine($"Lesson: {lesson.Title} is locked.");
            return;
        }

        // Use the NavigationService abstraction to perform the navigation.
        await _navigationService.NavigateToAsync(lesson.Route);
    }

    // --- Lesson Data and Logic Methods ---

    private void LoadLessonData()
    {
        // Define all lessons here with their routes. 
        // Note: I'm setting the first one as completed for demonstration 
        // purposes, so the second one is unlocked initially.
        Lessons.Add(new LessonInfo
        {
            LessonId = 1,
            Title = "Module 1: What is a Bit?",
            Route = nameof(BinaryLesson01),
            IsCompleted = true
        });

        Lessons.Add(new LessonInfo
        {
            LessonId = 2,
            Title = "Module 2: Counting in Binary",
            Route = nameof(BinaryLesson02)
        });

        Lessons.Add(new LessonInfo
        {
            LessonId = 3,
            Title = "Module 3: Bytes and Beyond",
            Route = nameof(HexLesson01)
        });

        Lessons.Add(new LessonInfo
        {
            LessonId = 4,
            Title = "Module 4: Decimal to Binary",
            Route = nameof(HexLesson02)
        });
    }

    private void UpdateLessonLockStatus()
    {
        for (int i = 0; i < Lessons.Count; i++)
        {
            // The first lesson (index 0) is always unlocked
            if (i == 0)
            {
                Lessons[i].IsLocked = false;
            }
            else
            {
                var previousLesson = Lessons[i - 1];

                // Set the current lesson's lock status based on the previous one's completion status.
                // This is the core sequential path logic.
                Lessons[i].IsLocked = !previousLesson.IsCompleted;
            }
        }
    }

    // This is the method that will be called from other ViewModels (or the MenuPage itself)
    // when a lesson is successfully completed, ensuring the menu updates.
    public void MarkLessonAsCompleted(int lessonId)
    {
        var lessonToUpdate = Lessons.FirstOrDefault(l => l.LessonId == lessonId);
        if (lessonToUpdate != null && !lessonToUpdate.IsCompleted)
        {
            lessonToUpdate.IsCompleted = true;

            // Recalculate locks to unlock the NEXT lesson.
            UpdateLessonLockStatus();

            // Optional: Save progress to local storage here.
        }
    }

    // Include the original Number Converter Command
    [RelayCommand]
    private async Task GotoNumberConverter()
    {
        await _navigationService.NavigateToAsync(nameof(NumberConverterPage));
    }
}