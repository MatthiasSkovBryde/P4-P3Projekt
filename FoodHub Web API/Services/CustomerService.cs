namespace FoodHub_Web_API.Services
{
    public class CustomerService
    {
        Task<List<StaticCustomerResponse>> GetAll();
        Task<DirectCustomerResponse> GetById(int customerId);
        Task<DirectCustomerResponse> Create(NewCustomerRequest request);
        Task<DirectCustomerResponse> Update(int customerId, NewCustomerRequest request);
        Task<DirectCustomerResponse> Delete(int customerId);

    }
}
