using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Bytes_Academy.Models
{
    // This class holds user data for database storage. It is a Model.
    public class UserModel // Making it public to be accessible across the application helps to manage user data effectively.
    {
        // { get; set; } properties for user data must be used for database storage and retrieval
        public string? UserName { get; set; } // Nullable string to allow for cases where the username might not be set
        public int Age { get; set; }
        public bool IsBinary01Complete { get; set; }
        public bool IsBinary02Complete { get; set; }
        public bool IsHex01Complete { get; set; }
        public bool IsHex02Complete { get; set; }
        public bool IsDarkModeEnabled { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int UserPoints { get; set; }
    }
}
