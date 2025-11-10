namespace Tiny_Bytes_Academy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // The following are route registrations for navigation within the app.
            Routing.RegisterRoute(nameof(Views.MenuPage), typeof(Views.MenuPage));
            Routing.RegisterRoute(nameof(Views.BinaryLesson01), typeof(Views.BinaryLesson01));
            Routing.RegisterRoute(nameof(Views.BinaryLesson02), typeof(Views.BinaryLesson02));
            Routing.RegisterRoute(nameof(Views.HexLesson01), typeof(Views.HexLesson01));
            Routing.RegisterRoute(nameof(Views.HexLesson02), typeof(Views.HexLesson02));
            Routing.RegisterRoute(nameof(Views.SettingsPage), typeof(Views.SettingsPage));
        }
    }
}
