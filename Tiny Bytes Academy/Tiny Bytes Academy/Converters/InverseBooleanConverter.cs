using System.Globalization;
using Microsoft.Maui.Controls;

namespace Tiny_Bytes_Academy.Converters;

// this file is primarily used to invert boolean values in data bindings
public class InverseBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return !boolean;
        }
        // If it's not a boolean, return the original value or false by default
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // One-way binding is sufficient for this scenario
        throw new NotSupportedException();
    }
}