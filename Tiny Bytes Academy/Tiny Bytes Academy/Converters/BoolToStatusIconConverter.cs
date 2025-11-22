using System.Globalization;
using Microsoft.Maui.Controls;

namespace Tiny_Bytes_Academy.Converters;

public class BoolToStatusIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolean)
        {
            // The parameter tells us what property we're checking (e.g., "IsCompleted" or "IsLocked")
            if (parameter is string statusType)
            {
                if (statusType == "IsCompleted")
                {
                    // If completed (true), show a checkmark. Otherwise, show nothing or a current indicator.
                    return boolean ? "✅" : "▶️";
                }
                else if (statusType == "IsLocked")
                {
                    // If locked (true), show a padlock. Otherwise, show nothing.
                    return boolean ? "🔒" : string.Empty;
                }
            }
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}