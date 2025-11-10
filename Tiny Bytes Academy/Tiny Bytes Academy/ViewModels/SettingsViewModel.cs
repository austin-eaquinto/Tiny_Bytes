using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Tiny_Bytes_Academy.ViewModels
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        const string ThemePrefKey = "app_theme_is_dark";

        bool _isDarkMode;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SettingsViewModel()
        {
            // Load persisted preference (default false = light)
            _isDarkMode = Preferences.Get(ThemePrefKey, false);
            ApplyTheme(_isDarkMode);

            ToggleThemeCommand = new Command(() => IsDarkMode = !IsDarkMode);
        }

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode == value) return;
                _isDarkMode = value;
                OnPropertyChanged();
                ApplyTheme(value);
                Preferences.Set(ThemePrefKey, value);
            }
        }

        public ICommand ToggleThemeCommand { get; }

        void ApplyTheme(bool dark)
        {
            // Apply the user theme. Unavailable Application.Current is guarded.
            if (Application.Current != null)
            {
                Application.Current.UserAppTheme = dark ? AppTheme.Dark : AppTheme.Light;
            }
        }

        void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
