using PET.Models;

namespace PET.Interfaces
{
    public interface IUser
    {
        Task AddUserAsync(Users user);
        Task<List<Users>> LoadAllUsersAsync();
        string HashPassword(string password);
        bool PasswordValidation(string enteredPassword, string storedPassword);
        Task UpdateUserAsync(Users user);

    }
}
