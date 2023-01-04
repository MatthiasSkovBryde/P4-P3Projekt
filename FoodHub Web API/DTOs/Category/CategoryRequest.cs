namespace FoodHub_Web_API.DTOs.Category
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(25, ErrorMessage = "Category name mist not contain more than 25 characters")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
