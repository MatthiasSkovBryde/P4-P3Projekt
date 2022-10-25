namespace FoodHub_Web_API.DTOs.Account
{
    public class AccountRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Username must be between")]
        public string Username { get; set; }

        [Required(ErrorMessage = "* Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email must not exceed more than 100 chars")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [StringLength(32, ErrorMessage = "* must not contain more than 32 chars")]
        public string Role { get; set; } = "Customer";
    }
}
