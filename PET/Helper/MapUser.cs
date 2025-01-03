using PET.Interfaces;
using PET.Models;

namespace PET.Helper
{
    public class MapUser
    {
        private readonly IUser UserService;

        // Constructor for dependency injection
        public MapUser(IUser userService)
        {
            UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<Users?> GetUserByUsernameAsync(PreferencesStoreClone storage)
        {
            try
            {
                // Retrieve the username from storage
                string username = storage.Get<string>("Username");

                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("No username found in storage.");
                    return null;
                }

                // Load all users
                var users = await UserService.LoadAllUsersAsync();

                // Find the user with the matching username
                var user = users.FirstOrDefault(u => u.UserName == username);

                if (user != null)
                {
                    Console.WriteLine($"User found: {user.Name}");
                }
                else
                {
                    Console.WriteLine("User not found.");
                }

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user by name: {ex.Message}");
                return null;
            }
        }
    }
}

