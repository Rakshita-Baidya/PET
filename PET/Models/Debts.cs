using System.ComponentModel.DataAnnotations;

namespace PET.Models
{
    public class Debts
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Users User_Id { get; set; }
        public string Source { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public DateTime Taken_Date { get; set; }
        public decimal Interest_Rate { get; set; }
        public DateTime Due_Date { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTime Date_Created { get; set; } = DateTime.Now;
    }
}
