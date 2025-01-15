using PET.Models;

namespace PET.Interfaces
{
    public interface IDebt
    {
        Task AddDebtAsync(Debts debt);
        Task<List<Debts>> LoadAllDebtsAsync();
        Task UpdateDebtAsync(Debts debt);
        Task DeleteDebtAsync(Debts debt);
        Task UpdateUserBalanceDebt(Users user, Debts debt);
        Task RevertUserBalanceDebt(Users user, Debts debt);
        Task ExportDebtsToExcelAsync(string filePath);
        Task<Debts> GetDebtByIdAsync(Guid debtId);
    }
}
