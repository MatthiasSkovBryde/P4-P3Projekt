
namespace FoodHub_Web_API.DTOs.Product
{
    public class DirectProductResponse
    {
        public int ProductID { get; set; } = 0;

        public string ProductName { get; set; } = string.Empty;

        public double ProductPrice { get; set; } = 0;

        public string ProductQuantity { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public StaticManufacturerResponse Manufacturer { get; set; } = null;

        public StaticCategoryResponse Category { get; set; } = null;

        public DateTime RealeseDate { get; set; }

        public StaticProductTypeResponse ProductType { get; set; } = null!;

        public StaticDiscountResponse? Discount { get; set; }

        public List<StaticTransactionResponse> Transactions { get; set; } = new();

        public List<StaticPhotoResponse> Photos { get; set; } = new();
    }
}
