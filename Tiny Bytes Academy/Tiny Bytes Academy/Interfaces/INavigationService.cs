using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Bytes_Academy.Interfaces;

// this interface defines a navigation service for navigating between views in a MAUI application.
public interface INavigationService
{
    // The ViewModel only requests navigation to a named route.
    // It doesn't know (or care) that this is executed via MAUI Shell.
    Task NavigateToAsync(string route);
}
