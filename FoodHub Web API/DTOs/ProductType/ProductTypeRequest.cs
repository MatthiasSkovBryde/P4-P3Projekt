namespace FoodHub_Web_API.DTOs.ProductType
{
    public class ProductTypeRequest
    {
        [Required(ErrorMessage = "ProductType name is required")]
        public string ProductTypeName { get; set; } = string.Empty;
    }
}
