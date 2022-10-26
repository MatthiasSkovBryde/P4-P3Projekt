namespace FoodHub_Web_API.Database
{
    /// <summary>
    /// Inherit from DbContext 
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        /// <summary>
        /// Adder vores Customer entity osv.
        /// </summary>
        public DbSet<Customer> Customer { get; set; } 
        public DbSet<Account> Account { get; set; }

        /// <summary>
        /// Creating models
        /// </summary>
        /// <param name="modelbuilder"></param>
        protected void OmModelCreating ( ModelBuilder modelbuilder)
        {
            // Create a model for Customer
            modelbuilder.Entity<Customer>( entity =>
            {
                entity.HasOne(e => e.Account).WithOne(e => e.Customer);
                entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()"); // Fanger datetime for hvornår entitien blev lavet i databasen. Sætter Created_At default til getdate()
                entity.HasIndex(e => e.PhoneNumber).IsUnique();
            });
            
            // Creating models for Account
            modelbuilder.Entity<Account>().HasData(
            new Account
            {
                AccountID = 1,
                Email = "test@test.com",
                Password = "Passw0rd"
            });
        }
    }
}
