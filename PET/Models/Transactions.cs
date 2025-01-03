using System.ComponentModel.DataAnnotations;

namespace PET.Models
{
    public class Transactions
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string UserName { get; set; }

        public Users User { get; set; }

        [Required]
        public string Title { get; set; }
        public String TransactionType { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public decimal Amount { get; set; }
        public String PaymentMethod { get; set; }
        public string TagName { get; set; }

        public Tags Tags { get; set; }
        public string? Notes { get; set; }

        public DateTime Date_Created { get; set; } = DateTime.Now;
    }
}
