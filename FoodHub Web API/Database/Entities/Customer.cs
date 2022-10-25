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
        [ForeignKey("Account.AccountID")]
        public int AccountID { get; set; }
        public Account Account { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public int PhoneNumber { get; set; }

        [Column(TypeName = "int")]
        public int ZipCode { get; set; } = 0;

        [Column(TypeName = "nvarchar(32)")]
        public int Gender { get; set; }

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
