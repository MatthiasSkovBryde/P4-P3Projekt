namespace FoodHub_Web_API.DTOs.Authentication
{
    public class StaticRefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public DateTime Expires_At { get; set; } = DateTime.UtcNow;
    }
}
