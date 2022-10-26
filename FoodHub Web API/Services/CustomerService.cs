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

        public async Task<DirectCustomerResponse> Delete(int customerId)
        {
            Customer customer = await _customerRepository.Delete(customerId);

            if (customer != null)
            {
                return _mapper.Map<DirectCustomerResponse>(customer);
            }
            return null;
        }

        public async Task<List<StaticCustomerResponse>> GetAll()
        {
            List<Customer> customer = await _customerRepository.GetAll();

            if (customer != null)
            {
                return customer.Select(customer => _mapper.Map<Customer, StaticCustomerResponse>(customer)).ToList();
            }
            return null;
        }

        public async Task<DirectCustomerResponse> GetById(int customerId)
        {
            Customer customer = await _customerRepository.GetById(customerId);

            if (customer != null)
            {
                return _mapper.Map<DirectCustomerResponse>(customer);
            }

            return null;
        }

        public async Task<DirectCustomerResponse> Update(int customerId, NewCustomerRequest request)
        {
            Account account = await _accountRepository.Update(request.Customer.AccountID, _mapper.Map<Account>(request.Account));
            if (account != null)
            {
                return null;
            }

            Customer customer = await _customerRepository.Update(customerId, _mapper.Map<Customer>(request.Customer));
            if (customer != null)
            {
                return _mapper.Map<DirectCustomerResponse>(customer);
            }

            return null;
        }
    }
}
