namespace FoodHub_Web_API.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetById(int accountId);
        Task<Account> Create(Account request);
        Task<Account> Update(int accountId, Account requset);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _context;

        /// <summary>
        /// Constuctor for AccountRepository
        /// </summary>
        /// <param name="context"></param>
        public AccountRepository( DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates an Account in the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Account> Create(Account request)
        {
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
                account.Modified_At = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            return account;
        }
    }
}
