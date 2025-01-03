using System.ComponentModel.DataAnnotations;

namespace PET.Models
{
    public class Currencies
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Exchange_Rate { get; set; }
    }
}
