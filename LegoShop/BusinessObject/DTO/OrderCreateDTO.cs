
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class OrderCreateDTO
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public string ShippingAddress { get; set; } = null!;
        [Required]
        public bool Craft { get; set; }
        [Required]
        public decimal Prices { get; set; }
        [Required]
        public int TotalQuantity { get; set; }
    }
}
