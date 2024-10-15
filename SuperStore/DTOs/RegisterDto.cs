using System.ComponentModel.DataAnnotations;

namespace SuperStore.DTOs
{
    public class RegisterDto
    {

        public string DisplayName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
            
    }
}
