using PET.Interfaces;
using PET.Models;
using System.Text.Json;


namespace PET.Services
{
    public class TransactionService : ITransaction
    {
        // path to transaction json file
        private readonly string transactionsFilePath = Path.Combine(AppContext.BaseDirectory, "Details", "Transactions.json");

        // Method to add a new transaction
        public async Task AddTransactionAsync(Transactions transaction)
        {
            try
            {
                var transactions = await LoadAllTransactionsAsync();
                transactions.Add(transaction);
                await SaveAllTransactionAsync(transactions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding transaction: {ex.Message}");
                throw;
            }
        }
        // Method to save the entire transaction list to the json file
        private async Task SaveAllTransactionAsync(List<Transactions> transactions)
        {
            try
            {
                // Convert the transaction list to a JSON string with indentation
                var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(transactionsFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving transactions: {ex.Message}");
                throw;
            }
        }

        // Method to load transactions from the json file
        public async Task<List<Transactions>> LoadAllTransactionsAsync()
        {
            try
            {
                if (!File.Exists(transactionsFilePath))
                {
                    return new List<Transactions>();
                }
                var json = await File.ReadAllTextAsync(transactionsFilePath);
                return JsonSerializer.Deserialize<List<Transactions>>(json) ?? new List<Transactions>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading transactions: {ex.Message}");
                return new List<Transactions>();
            }
        }

        public async Task<Transactions> GetTransactionByIdAsync(Guid transactionId)
        {
            try
            {
                var transactions = await LoadAllTransactionsAsync();
                return transactions.FirstOrDefault(t => t.Id == transactionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching transaction by ID: {ex.Message}");
                return null;
            }
        }

        // Method to update user balance
        public async Task UpdateUserBalanceTransaction(Users user, Transactions transaction)
        {
            if (transaction.TransactionType == "Debit")
            {
                if (transaction.Amount > user.Balance)
                {
                    throw new Exception("Insufficient balance for this debit transaction.");
                }
                user.Balance -= transaction.Amount;
            }
            else if (transaction.TransactionType == "Credit")
            {
                user.Balance += transaction.Amount;
            }
        }

        // Method to revert user balance
        public async Task RevertUserBalanceTransaction(Users user, Transactions transaction)
        {
            if (transaction.TransactionType == "Debit")
            {
                user.Balance += transaction.Amount;
            }
            else if (transaction.TransactionType == "Credit")
            {
                user.Balance -= transaction.Amount;
            }
        }

        // Method to update an existing transaction
        public async Task UpdateTransactionAsync(Transactions updatedTransaction)
        {
            try
            {
                var transactions = await LoadAllTransactionsAsync();
                var transactionIndex = transactions.FindIndex(t => t.Id == updatedTransaction.Id);

                // If found, replace the old transaction with the updated one
                if (transactionIndex != -1)
                {
                    transactions[transactionIndex] = updatedTransaction;
                    await SaveAllTransactionAsync(transactions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating transaction: {ex.Message}");
                throw;
            }
        }

        // Method to delete a transaction
        public async Task DeleteTransactionAsync(Transactions transactionToDelete)
        {
            try
            {
                var transactions = await LoadAllTransactionsAsync();
                transactions.RemoveAll(t => t.Id == transactionToDelete.Id);
                await SaveAllTransactionAsync(transactions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting transaction: {ex.Message}");
                throw;
            }
        }
    }
}
