using System.ComponentModel.DataAnnotations;

namespace StoreAPI.DTO
{
    public class RegisterDTO
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
    }
}
