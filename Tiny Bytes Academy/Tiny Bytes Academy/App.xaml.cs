namespace Tiny_Bytes_Academy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Ensure MainPage is NOT set here if you're using CreateWindow
            // MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = new Window(new AppShell()); // Pass AppShell directly to the Window constructor

            // --- PLATFORM-SPECIFIC WINDOW SIZE ---
#if WINDOWS // tells the compiler: "the code between here and the #endif line should only be included if you are currently building the project for the Windows platform."
            // Set specific size for Windows
            window.Width = 1024; // Desired width for Windows
            window.Height = 768; // Desired height for Windows
            window.Title = "Tiny Bytes Academy - Windows";
            // You can also set MinimumWidth, MinimumHeight, MaximumWidth, MaximumHeight here for Windows
#elif MACCATALYST
            // Optional: Set specific size for Mac Catalyst if desired
            window.Width = 900;
            window.Height = 700;
            window.Title = "Tiny Bytes Academy - macOS";
#elif IOS || ANDROID
            // For mobile platforms, you generally don't set fixed window sizes.
            // The OS manages the app's full screen.
            // You might still set a Title if you want it to appear in task switchers.
            window.Title = "Tiny Bytes Academy - Mobile";
#else
            // Fallback for other platforms or if no specific size is needed
            // The window will take its default size, which might be full screen or system-defined.
            window.Width = 600; // Example for other platforms if desired
            window.Height = 400;
            window.Title = "Tiny Bytes Academy";
#endif

            // Ensure the window has content (very important!)
            // base.CreateWindow(activationState) might give a window with no page
            // so explicitly set it if you're not passing it to the Window constructor directly.
            //if (window.Page == null)
            //{
            //    window.Page = new AppShell(); // Set your main page here
            //}


            return window;
        }
    }
}