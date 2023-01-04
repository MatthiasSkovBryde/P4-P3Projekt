﻿namespace FoodHub_Web_API.Database.Entities
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string CategoryName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created_At { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Modified_At { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}