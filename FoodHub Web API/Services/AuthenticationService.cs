namespace FoodHub_Web_API.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Authenticate( AuthenticationRequest request, string ipAddress );
        Task<AuthenticationResponse> RefreshToken(string token, string ipAddress);
        Task<bool> RevokeToken(string token, string ipAddress);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Contructor for AuthenticationService
        /// </summary>
        /// <param name="accountRepository"></param>
        /// <param name="mapper"></param>
        public AuthenticationService(IAuthenticationRepository authenticationRepository, IMapper mapper)
        {
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Making a response.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ipAddress"></param>
        /// <returns>authenticationResponse</returns>
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, string ipAddress)
        {
            AuthenticationResponse authenticationResponse = await _authenticationRepository.Authenticate(request.Email, request.Password, ipAddress); // Gets AuthenticationResponse from Repository

            if (authenticationResponse != null)
            {
                return authenticationResponse;
            }
            return null;
        }

        /// <summary>
        /// Making a response token.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ipAddress"></param>
        /// <returns>authenticationResponse</returns>
        public async Task<AuthenticationResponse> RefreshToken(string token,  string ipAddress)
        {
            AuthenticationResponse authenticationResponse = await _authenticationRepository.RefreshToken(token, ipAddress);

            if (authenticationResponse != null)
            {
                return authenticationResponse;
            }

            return null;
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            return await _authenticationRepository.RevokeToken(token, ipAddress);
        }
    }
}
