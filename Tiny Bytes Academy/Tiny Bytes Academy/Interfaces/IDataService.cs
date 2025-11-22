using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiny_Bytes_Academy.Models;

namespace Tiny_Bytes_Academy.Interfaces
{
    // Interface for data services handling user profile loading and saving
    // Interfaces define contracts for classes to implement, ensuring consistency across different implementations
    // Interface means this is a public contract that can be implemented by any class, promoting flexibility and scalability in the application design
    public interface IDataService // IDataService defines methods for loading and saving user profile data
    {
        // 1. Initial setup/loading logic
        Task<UserModel> LoadUserProfileAsync();

        // 2. Saving the current state
        Task SaveUserProfileAsync(UserModel userProfile);
    }
}
