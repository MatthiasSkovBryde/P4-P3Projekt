namespace FoodHub_Web_API.DTOs.Customer
{
    public class DirectCustomerResponse
    {
        public int CustomerID { get; set; }

        public StaticAccountResponse Account { get; set; } = null!;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int ZipCode { get; set; } = 0;

        public DateTime Created_At { get; set; }
    }
}
