using PET.Models;

namespace PET.Interfaces
{
    public interface ITransaction
    {
        Task SaveTransactionAsync(Transactions transaction);
        Task<List<Transactions>> LoadAllTransactionsAsync();
        Task UpdateTransactionAsync(Transactions transaction);
        Task DeleteTransactionAsync(Transactions transaction);

        Task<Transactions> GetTransactionByIdAsync(Guid transactionId);
    }
}
