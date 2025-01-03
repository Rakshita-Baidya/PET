using PET.Models;

namespace PET.Interfaces
{
    public interface IUser
    {
        Task SaveUserAsync(Users user);
        Task<List<Users>> LoadAllUsersAsync();
        string HashPassword(string password);
        bool PasswordValidation(string enteredPassword, string storedPassword);

        ////Task DeleteUserAsync(User user);
        //Task UpdateUserAsync(User user);

    }
}
