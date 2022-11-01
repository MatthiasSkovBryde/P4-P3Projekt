namespace FoodHub_Web_API.Database.Entities
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Column(TypeName = "nvarchar(64)")]
        public string Password { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "datetime2")]
        public DateTime Created_At { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Modified_At { get; set; }

        /// <summary>
        /// Navigation reference
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Navigation reference
        /// </summary>
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
