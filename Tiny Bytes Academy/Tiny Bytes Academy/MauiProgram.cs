using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Tiny_Bytes_Academy
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    // Added fonts - ensure these files exist in Resources/Fonts
                    fonts.AddFont("VT323-Regular.ttf", "VT323");
                    fonts.AddFont("PressStart2P-Regular.ttf", "PressStartP2");
                    fonts.AddFont("Montserrat-Regular.ttf", "Montserrat");
                });

#if DEBUG
                builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
