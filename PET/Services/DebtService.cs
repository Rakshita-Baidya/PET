using OfficeOpenXml;
using PET.Interfaces;
using PET.Models;
using System.Text.Json;

namespace PET.Services
{
    public class DebtService : IDebt
    {
        // path to debt json file
        private readonly string debtsFilePath = Path.Combine(AppContext.BaseDirectory, "Details", "Debts.json");

        // Method to save a new debt
        public async Task AddDebtAsync(Debts debt)
        {
            try
            {
                var debts = await LoadAllDebtsAsync();
                debts.Add(debt);
                await SaveAllDebtAsync(debts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving debt: {ex.Message}");
                throw;
            }
        }

        // Method to save the entire debt list to the json file
        private async Task SaveAllDebtAsync(List<Debts> debts)
        {
            try
            {
                // Convert the debt list to a JSON string with indentation
                var json = JsonSerializer.Serialize(debts, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(debtsFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving debts: {ex.Message}");
                throw;
            }
        }

        // Method to load debts from the json file
        public async Task<List<Debts>> LoadAllDebtsAsync()
        {
            try
            {
                if (!File.Exists(debtsFilePath))
                {
                    return new List<Debts>();
                }
                var json = await File.ReadAllTextAsync(debtsFilePath);
                return JsonSerializer.Deserialize<List<Debts>>(json) ?? new List<Debts>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading debts: {ex.Message}");
                return new List<Debts>();
            }
        }

        public async Task<Debts> GetDebtByIdAsync(Guid debtId)
        {
            try
            {
                var debts = await LoadAllDebtsAsync();
                return debts.FirstOrDefault(t => t.Id == debtId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching debt by ID: {ex.Message}");
                return null;
            }
        }

        // Method to update user balance
        public async Task UpdateUserBalanceDebt(Users user, Debts debt)
        {
            if (debt.Is_Cleared)
            {
                if (debt.Amount > user.Balance)
                {
                    throw new Exception("Insufficient balance for clearing debt.");
                }
                user.Balance -= debt.Amount;
            }
            else
            {
                user.Balance += debt.Amount;
            }
        }

        // Method to revert user balance
        public async Task RevertUserBalanceDebt(Users user, Debts debt)
        {
            if (!debt.Is_Cleared)
            {
                user.Balance -= debt.Amount;
            }
            else
            {
                user.Balance += debt.Amount;
            }
        }


        // Method to update an existing debt
        public async Task UpdateDebtAsync(Debts updatedDebt)
        {
            try
            {
                var debts = await LoadAllDebtsAsync();
                var debtIndex = debts.FindIndex(t => t.Id == updatedDebt.Id);

                // If found, replace the old debt with the updated one
                if (debtIndex != -1)
                {
                    debts[debtIndex] = updatedDebt;
                    await SaveAllDebtAsync(debts);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating debt: {ex.Message}");
                throw;
            }
        }

        //export to excel
        public async Task ExportDebtsToExcelAsync(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                var debts = await LoadAllDebtsAsync();
                if (debts == null || !debts.Any())
                {
                    Console.WriteLine("No debts available to export.");
                    return;
                }

                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Debts");

                // Add headers
                worksheet.Cells[1, 1].Value = "Source";
                worksheet.Cells[1, 2].Value = "Amount";
                worksheet.Cells[1, 3].Value = "Taken Date";
                worksheet.Cells[1, 4].Value = "Interest Rate";
                worksheet.Cells[1, 5].Value = "Due Date";
                worksheet.Cells[1, 6].Value = "Status";
                worksheet.Cells[1, 7].Value = "Notes";

                // Add data rows
                for (int i = 0; i < debts.Count; i++)
                {
                    var debt = debts[i];
                    worksheet.Cells[i + 2, 1].Value = debt.Source;
                    worksheet.Cells[i + 2, 2].Value = $"{debt.Amount:C}";
                    worksheet.Cells[i + 2, 3].Value = debt.Taken_Date.ToShortDateString();
                    worksheet.Cells[i + 2, 4].Value = debt.Interest_Rate;
                    worksheet.Cells[i + 2, 5].Value = debt.Due_Date.ToShortDateString();
                    worksheet.Cells[i + 2, 6].Value = debt.Is_Cleared ? "Cleared" : "Pending";
                    worksheet.Cells[i + 2, 7].Value = debt.Notes;
                }

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Save workbook
                await File.WriteAllBytesAsync(filePath, package.GetAsByteArray());
                Console.WriteLine($"Excel file saved successfully at {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting debts to Excel: {ex.Message}");
            }
        }

        // Method to delete a debt
        public async Task DeleteDebtAsync(Debts debtToDelete)
        {
            try
            {
                var debts = await LoadAllDebtsAsync();
                debts.RemoveAll(t => t.Id == debtToDelete.Id);
                await SaveAllDebtAsync(debts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting debt: {ex.Message}");
                throw;
            }
        }
    }
}
