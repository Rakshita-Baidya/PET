using PET.Interfaces;
using PET.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PET.Services
{

    public class UserService : IUser
    {
        // Path to users.json file
        private readonly string usersFilePath = Path.Combine(AppContext.BaseDirectory, "Details", "Users.json");

        //private string HashPassword(string password)
        //{
        //    // Generate bcrypt hash using the password
        //    return BCrypt.Net.BCrypt.HashPassword(password);
        //}

        //public bool PasswordValidation(string enteredPassword, string storedPassword)
        //{
        //    // Compare the provided password with the stored bcrypt hash
        //    return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);
        //}

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool PasswordValidation(string enteredPassword, string storedPassword)
        {
            string hashedPassword = HashPassword(enteredPassword);
            if (hashedPassword == storedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // Method to save a new user
        public async Task SaveUserAsync(Users user)
        {
            try
            {
                var users = await LoadAllUsersAsync();
                users.Add(user);
                await SaveAllUsersAsync(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user: {ex.Message}");
                throw;
            }
        }

        // Private method to save all users to the JSON file
        private async Task SaveAllUsersAsync(List<Users> users)
        {
            try
            {
                // Serialize the user list to JSON
                var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                // Write the JSON string to the file
                await File.WriteAllTextAsync(usersFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving users: {ex.Message}");
                throw;
            }
        }


        // Method to load users from the JSON file
        public async Task<List<Users>> LoadAllUsersAsync()
        {
            try
            {
                if (!File.Exists(usersFilePath))
                {
                    return new List<Users>();
                }
                var json = await File.ReadAllTextAsync(usersFilePath);
                return JsonSerializer.Deserialize<List<Users>>(json) ?? new List<Users>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users: {ex.Message}");
                return new List<Users>();
            }
        }

        // Method to update an existing user
        public async Task UpdateUserAsync(Users updatedUser)
        {
            try
            {
                var users = await LoadAllUsersAsync();
                var userIndex = users.FindIndex(u => u.Id == updatedUser.Id);
                if (userIndex != -1)
                {
                    users[userIndex] = updatedUser;
                    await SaveAllUsersAsync(users);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                throw;
            }
        }


        //// Method to delete a user
        //public async Task DeleteUserAsync(User userToDelete)
        //{
        //    try
        //    {
        //        var users = await LoadAllUsersAsync();
        //        users.RemoveAll(u => u.Id == userToDelete.Id);
        //        await SaveUsersAsync(users);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error deleting user: {ex.Message}");
        //        throw;
        //    }
        //}

    }
}
