using System.ComponentModel.DataAnnotations;

namespace PET.Models
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        public Currencies Currency { get; set; }
        [Required]
        public double Balance { get; set; }
        public DateTime Date_Created { get; set; } = DateTime.Now;
    }
}
