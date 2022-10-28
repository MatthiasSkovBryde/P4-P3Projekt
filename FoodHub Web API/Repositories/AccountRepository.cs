
namespace FoodHub_Web_API.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetById(int accountId);
        Task<Account> Create(Account request);
        Task<Account> Update(int accountId, Account requset);
        Task<AuthenticationResponse> Authenticate(string email, string password, string ipAddress);
        Task<AuthenticationResponse> RefreshToken (string token, string ipAddress);
        Task<bool> RevokeToken(string token, string ipAddress);
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

        /// <summary>
        /// Checking for the clients username, email, password and if you have a JWTToken and a RefreshToken.
        /// </summary>
        /// <param name="username_email"></param>
        /// <param name="password"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<AuthenticationResponse> Authenticate(string email, string password, string ipAddress)
        {
            Account account = await _context.Account.Include(e => e.Customer).FirstOrDefaultAsync(x => x.Email == email);

            if (account == null)
            {
                return null;
            }

            if (!BC.Verify(password, account.Password))
            {
                return null;
            }

            string accessToken = JWTHandler.GenerateJWTToken(account, _appSettings);
            RefreshToken refreshToken = JWTHandler.GenerateRefreshToken(ipAddress);

            account.RefreshTokens.Add(refreshToken);

            _context.Update(account);
            await _context.SaveChangesAsync();

            return new AuthenticationResponse(refreshToken.Token, accessToken);
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

        public async Task<AuthenticationResponse> RefreshToken(string token, string ipAddress)
        {
            Account account = await _context.Account.Include(e => e.RefreshTokens).Include(e => e.Customer).FirstOrDefaultAsync(c => c.RefreshTokens.Any(t => t.Token == token));

            if (account == null)
            {
                return null;
            }

            RefreshToken refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                return null;
            }

            RefreshToken newRefreshToken = JWTHandler.GenerateRefreshToken(ipAddress);

            refreshToken.Revoked_At = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            account.RefreshTokens.Add(newRefreshToken);

            _context.Update(account);
            _context.Update(refreshToken);
            await _context.SaveChangesAsync();

            string accessToken = JWTHandler.GenerateJWTToken(account, _appSettings);

            return new AuthenticationResponse(newRefreshToken.Token, accessToken);
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

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            Account account = await _context.Account.Include(e => e.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (account == null)
            {
                return false;
            }

            RefreshToken refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.Revoked_At = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            return true;

        }
    }
}
