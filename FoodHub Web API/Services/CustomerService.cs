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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;


        /// <summary>
        /// Constuctor of CustomerService
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="mapper"></param>
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<DirectCustomerResponse> Create(NewCustomerRequest request)
        {
            Account account = await _accountRepository.Create( _mapper.Map<Account>(request.Account));
            if (account == null)
            {
                return null;
            }

            request.Customer.AccountID = account.AccountID;

            Customer customer = await _customerRepository.Create(_mapper.Map<Customer>(request.Customer));
            if (customer != null)
            {
                return _mapper.Map<DirectCustomerResponse>(customer);
            }
            return null;
        }

        public Task<DirectCustomerResponse> Delete(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<StaticCustomerResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<DirectCustomerResponse> GetById(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<DirectCustomerResponse> Update(int customerId, NewCustomerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
