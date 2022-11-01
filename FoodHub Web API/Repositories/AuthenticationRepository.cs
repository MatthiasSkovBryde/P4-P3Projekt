namespace FoodHub_Web_API.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticationResponse> Authenticate(string email, string password, string ipAddress);
        Task<AuthenticationResponse> RefreshToken(string token, string ipAddress);
        Task<bool> RevokeToken(string token, string ipAddress);
    }

    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DatabaseContext _context;
        private readonly AppSettings _appSettings;

        public AuthenticationRepository(DatabaseContext context, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        /// <summary>
        /// Checking for the clients email & password and if you have a JWTToken and a RefreshToken.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="ipAddress"></param>
        /// <returns>AuthenticationResponse(refreshToken.Token, accessToken);</returns>
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
        /// Crates a RefreshToken
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ipAddress"></param>
        /// <returns>AuthenticationResponse (newRefreshToken.Token, accessToken)</returns>
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

            account.RefreshTokens.Add(refreshToken);

            _context.Update(account);
            _context.Update(refreshToken);
            await _context.SaveChangesAsync();

            string accessToken = JWTHandler.GenerateJWTToken(account, _appSettings);

            return new AuthenticationResponse(newRefreshToken.Token, accessToken);

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
            refreshToken.CreatedByIp = ipAddress;

            _context.Update(account);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
