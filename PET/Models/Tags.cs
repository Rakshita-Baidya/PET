using System.ComponentModel.DataAnnotations;

namespace PET.Models
{
    public class Tags
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}
