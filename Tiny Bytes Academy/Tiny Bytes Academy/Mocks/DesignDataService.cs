using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiny_Bytes_Academy.Interfaces;
using Tiny_Bytes_Academy.Models;

namespace Tiny_Bytes_Academy.Mocks
{
    // This design-time data service provides dummy data for use in the UI designer
    public class DesignDataService : IDataService
    {
        public Task<UserModel> LoadUserProfileAsync()
        {
            // Return a default UserModel with dummy data for the designer
            return Task.FromResult(new UserModel
            {
                UserName = "Axolotl Enthusiast",
                Age = 8,
                // Score = 1200
            });
        }

        public Task SaveUserProfileAsync(UserModel user)
        {
            // Do nothing during design time
            return Task.CompletedTask;
        }
    }
}
