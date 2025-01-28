using PET.Models;

namespace PET.Interfaces
{
    public interface ITransaction
    {
        Task AddTransactionAsync(Transactions transaction);
        Task<List<Transactions>> LoadAllTransactionsAsync();
        Task UpdateTransactionAsync(Transactions transaction);
        Task DeleteTransactionAsync(Transactions transaction);
        Task UpdateUserBalanceTransaction(Users user, Transactions transaction);
        Task RevertUserBalanceTransaction(Users user, Transactions transaction);
        Task ExportTransactionsToExcelAsync(List<Transactions> filteredTransactions, string filePath);
        Task<Transactions> GetTransactionByIdAsync(Guid transactionId);
    }
}
