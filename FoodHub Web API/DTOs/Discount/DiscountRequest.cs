namespace FoodHub_Web_API.DTOs.Discount
{
    public class DiscountRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Desciption { get; set; } = string.Empty;

        [Required(ErrorMessage = "Discount percentage is required")]
        public int DiscountPercent { get; set; } = 0;

        [Required(ErrorMessage = "Expiration date is required")]
        public DateTime Revoked_At { get; set; }
    }
}
