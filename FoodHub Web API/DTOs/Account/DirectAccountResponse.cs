
namespace FoodHub_Web_API.DTOs.Account
{
    public class DirectAccountResponse
    {
        public int AccountID { get; set; } = 0;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public StaticCustomerResponse Customer { get; set; } = null!;
    }
}
