namespace FoodHub_Web_API.Services
{
    public interface ICustomerService
    {
        Task<List<StaticCustomerResponse>> GetAll();
        Task<DirectCustomerResponse> GetById(int customerId);
        Task<DirectCustomerResponse> Create(NewCustomerRequest request);
        Task<DirectCustomerResponse> Update(int customerId, NewCustomerRequest request);
        Task<DirectCustomerResponse> Delete(int customerId);
    }
    
    /// <summary>
    /// CustomerService is used to transfer data to and from CustomerRepository and CustomerController
    /// </summary>
    public class CustomerService
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;


        /// <summary>
        /// Constuctor of CustomerService
        /// </summary>
        /// <param name="customerRepository"></param>
        public CustomerService(ICustomerService customerRepository)
        {
            _customerService = customerRepository;
        }

        public async Task<List<StaticCustomerResponse>> Create( NewCustomerRequest request)
        {
            Account account = await _customerService.Create(request.Account);
        }
    }
}
