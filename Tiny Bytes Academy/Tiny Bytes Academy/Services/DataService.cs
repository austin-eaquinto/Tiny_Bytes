using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Models;

namespace Tiny_Bytes_Academy.Services
{
    // Implementation of IDataService for loading and saving user profile data
    // This class handles reading from and writing to a JSON file in the app's local storage
    public class DataService : IDataService // DataService implementation for user profile data management
    {
        private readonly string _filePath; // Path to the JSON file storing user profile data

        public DataService() // Constructor initializes the file path for user profile storage
        {
            // Using MAUI's FileSystem to get the app data directory
            _filePath = Path.Combine(
                Microsoft.Maui.Storage.FileSystem.AppDataDirectory,
                "userprofile.json");
        }

        public async Task<UserModel> LoadUserProfileAsync() // Load user profile data from the JSON file
        {
            // Check if the file exists; if not, return a new UserModel instance
            if (!File.Exists(_filePath))
            {
                return new UserModel(); // Return a new user profile if none exists
            }
            var json = await File.ReadAllTextAsync(_filePath); // Read the JSON content from the file
            return System.Text.Json.JsonSerializer.Deserialize<UserModel>(json) ?? new UserModel(); // Deserialize JSON to UserModel or return a new instance if deserialization fails
        }

        public async Task SaveUserProfileAsync(UserModel userProfile) // Save user profile data to the JSON file
        {
            var json = System.Text.Json.JsonSerializer.Serialize(userProfile); // Serialize the UserModel to JSON format
            await File.WriteAllTextAsync(_filePath, json); // Write the JSON content to the file
        }
    }
}
