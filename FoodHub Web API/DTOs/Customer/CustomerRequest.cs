namespace FoodHub_Web_API.DTOs.Customer
{
    public class CustomerRequest
    {
        [Required(ErrorMessage = "* AccountID is required")]
        public int AccountID { get; set; }

        [Required(ErrorMessage = "* FirstName is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "* LastName is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "* PhoneNumber is required")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "* must be 8 characters")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "* ZipCode is required")]
        public int ZipCode { get; set; }
    }
}
