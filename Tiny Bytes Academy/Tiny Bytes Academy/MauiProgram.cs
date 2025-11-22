using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Models;
using Tiny_Bytes_Academy.Services;
using Microsoft.Extensions.DependencyInjection;
using Tiny_Bytes_Academy.ViewModels;
using Tiny_Bytes_Academy.Views;

namespace Tiny_Bytes_Academy // a namespace is for organizing code and preventing name collisions, which means that classes with the same name can exist in different namespaces without conflict
{
    // MauiProgram sets up the MAUI application, including services, fonts, and logging
    // the definition of logging in this context is the process of recording information about the application's execution, which can be useful for debugging and monitoring
    public static class MauiProgram // Static class to configure and create the MAUI application
    {
        // Method to create and configure the MAUI application
        public static MauiApp CreateMauiApp()
        {
            // Create a MAUI app builder
            var builder = MauiApp.CreateBuilder();
            builder // Configure the builder
                .UseMauiApp<App>() // Specify the main application class
                .UseMauiCommunityToolkit() // Use the Community Toolkit for additional features like MVVM support
                .ConfigureFonts(fonts => // Configure custom fonts
                {
                    // Default MAUI fonts
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    // Added fonts - ensure these files exist in Resources/Fonts
                    fonts.AddFont("VT323-Regular.ttf", "VT323");
                    fonts.AddFont("PressStart2P-Regular.ttf", "PressStartP2");
                    fonts.AddFont("Montserrat-Regular.ttf", "Montserrat");
                });

            // A singleton is useful when exactly one object is needed to coordinate actions and data across the application
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<MenuPage>();
            builder.Services.AddSingleton<IDataService, DataService>();
            builder.Services.AddSingleton<UserModel>();

            // Using AddTransient allows the instance to be cleaned up and released from memory when the page is navigated away from
            builder.Services.AddTransient<MenuPageViewModel>();

            // Register all lesson ViewModels as Transient
            builder.Services.AddTransient<BinaryLesson01ViewModel>();
            builder.Services.AddTransient<BinaryLesson02ViewModel>();
            builder.Services.AddTransient<HexLesson01ViewModel>();
            builder.Services.AddTransient<HexLesson02ViewModel>();
            builder.Services.AddTransient<StartPageViewModel>();

            // You should also register the corresponding Views if you intend to use DI to construct them
            builder.Services.AddTransient<BinaryLesson01>();
            builder.Services.AddTransient<BinaryLesson02>();
            builder.Services.AddTransient<HexLesson01>();
            builder.Services.AddTransient<HexLesson02>();


#if DEBUG // Conditional compilation for debug mode
            builder.Logging.AddDebug(); // Add debug logging in debug mode
#endif

            return builder.Build(); // Build and return the configured MAUI application
        }
    }
}
