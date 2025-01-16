using System.ComponentModel.DataAnnotations;

namespace PET.Models
{
    public class Debts
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string UserName { get; set; }

        public Users User { get; set; }
        public string Source { get; set; }
        [Required]
        public double Amount { get; set; }
        public DateTime Taken_Date { get; set; } = DateTime.Now;
        public decimal Interest_Rate { get; set; }
        public double Interest_Amount { get; set; } = 0.00;
        public DateTime Due_Date { get; set; } = DateTime.Now;
        public string Notes { get; set; }
        public bool Is_Cleared { get; set; }
        public DateTime? Date_Cleared { get; set; }
        public DateTime Date_Created { get; set; } = DateTime.Now;
    }
}
