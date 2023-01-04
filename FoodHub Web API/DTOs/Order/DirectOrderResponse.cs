namespace FoodHub_Web_API.DTOs.Order
{
    public class DirectOrderResponse
    {
        public int OrderID { get; set; } = 0;

        public StaticCustomerResponse Customer { get; set; } = new();

        public double OrderTotal { get; set; } = 0;

        public List<StaticTransactionResponse> Transactoins { get; set; } = new List<StaticTransactionResponse>();
    }
}
