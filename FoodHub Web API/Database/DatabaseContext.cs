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
        public DbSet<RefreshToken> RefreshToken { get; set; }


        /// <summary>
        /// Creating models
        /// </summary>
        /// <param name="modelbuilder"></param>
        protected override void OnModelCreating ( ModelBuilder modelBuilder)
        {
            // Create a model for Customer
            modelBuilder.Entity<Customer>( entity =>
            {
                entity.HasIndex(e => e.PhoneNumber).IsUnique();
                entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()"); // Fanger datetime for hvornår entitien blev lavet i databasen. Sætter Created_At default til getdate()
            });

            // Creating models for Account
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
            });

            // Capturing the datetime when the entities was createt in the database. Sets Created_At default to getdate()
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
            });
        }
    }
}
