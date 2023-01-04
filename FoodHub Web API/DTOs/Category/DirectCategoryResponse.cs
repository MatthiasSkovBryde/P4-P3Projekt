namespace FoodHub_Web_API.DTOs.Category
{
    public class DirectCategoryResponse
    {
        public int CategoryID { get; set; } = 0;

        public string categoryName { get; set; } = string.Empty;

        public List<StaticProductResponse> Products { get; set; } = new();


    }
}
