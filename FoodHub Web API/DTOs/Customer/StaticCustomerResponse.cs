namespace FoodHub_Web_API.DTOs.Customer
{
    public class StaticCustomerResponse
    {
        public int CustomerID { get; set; }
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryID { get; set; }
        public int ZipCode { get; set; }
        public string Gender { get; set; }
        public DateTime Created_At { get; set; }
    }
}
