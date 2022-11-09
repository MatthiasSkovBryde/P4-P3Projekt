namespace FoodHub_Web_API.Database.Entities
{
    public class Customer
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public int CustomerID { get; set; }

        /// <summary>
        /// Fireign Key
        /// </summary>
        public int AccountID { get; set; }
        public Account Account { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string LastName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public int ZipCode { get; set; } = 0;

        /// <summary>
        /// Navigation reference
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime Created_At { get; set; }

        /// <summary>
        /// Navigation reference
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime Modified_At { get; set; }
    }
}
