namespace FoodHub_Web_API.DTOs.Authentication
{
    public class AuthenticationResponse
    {
        public string RefreshToken { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public AuthenticationResponse(string refreshToken, string accessToken)        {
            RefreshToken = refreshToken;
            AccessToken = accessToken;
        }
    }
}
