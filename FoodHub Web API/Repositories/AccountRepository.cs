namespace FoodHub_Web_API.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAll();
        Task<Account> GetById(int accountId);
        Task<Account> Create(Account request);
        Task<Account> Update(int accountId, Account requset);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _context;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constuctor for AccountRepository
        /// </summary>
        /// <param name="context"></param>
        public AccountRepository( DatabaseContext context, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<List<Account>> GetAll()
        {
            return await _context.Account.Include(x => x.Customer).ToListAsync();
        }

        /// <summary>
        /// Creates an Account in the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns>DirectResponse</returns>
        public async Task<Account> Create(Account request)
        {
            request.Password = BC.HashPassword(request.Password);
            _context.Account.Add(request);
            await _context.SaveChangesAsync();
            return await GetById(request.AccountID);
        }

        /// <summary>
        /// Gets an account by its accountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<Account> GetById(int accountId)
        {
            return await _context.Account.Include(x => x.Customer).FirstOrDefaultAsync(x => x.AccountID == accountId);
        }

        public async Task<Account> Update(int accountId, Account requset)
        {
            Account account = await GetById(accountId);
            if (account != null)
            {
                account.Email = requset.Email;
                account.Role = requset.Role;
                account.Modified_At = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            return account;
        }
    }
}
