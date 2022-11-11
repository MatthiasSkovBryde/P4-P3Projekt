namespace FoodHub_Web_API.Services
{
    public interface IAccountService
    {
        Task<List<StaticAccountResponse>> GetAll();
        Task<DirectAccountResponse> GetById(int accountId);
        Task<DirectAccountResponse> Update(int accountId, AccountRequest request);
    }

    public class AccountService
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<List<StaticAccountResponse>> GetAll()
        {
            List<Account> accounts = await _accountRepository.GetAll();

            if (accounts != null)
            {
                return accounts.Select(accounts => _mapper.Map<Account, StaticAccountResponse>(accounts)).ToList();
            }
            return null;
        }

        public async Task<DirectAccountResponse?> GetById(int accountId)
        {
            Account account = await _accountRepository.GetById(accountId);

            if (account != null)
            {
                return _mapper.Map<DirectAccountResponse>(account);
            }
            return null;
        }

        public async Task<DirectAccountResponse> Update(int accountId, AccountRequest request)
        {
            Account account = await _accountRepository.Update(accountId, _mapper.Map<Account>(request));

            if (account != null)
            {
                return _mapper.Map<DirectAccountResponse>(account);
            }
            return null;
        }
    }
}
