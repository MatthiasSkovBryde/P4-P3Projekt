namespace FoodHub_Web_API.Repositories
{
    /// <summary>
    /// Interface with methods
    /// </summary>
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetById(int customerId);
        Task<Customer> Create(Customer request);
        Task<Customer> Update(int customerId, Customer request);
        Task<Customer> Delete(int customerId);
    }
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _context;

        /// <summary>
        /// Constuctor for CustomerRepository
        /// </summary>
        /// <param name="context"></param>
        public CustomerRepository(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Customer> Create(Customer request)
        {
            _context.Customer.Add(request);
            await _context.SaveChangesAsync();
            return await GetById(request.CustomerID);
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Customer> Delete(int customerId)
        {
            Customer customer = await GetById(customerId);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return customer;
        }

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customer
                .Include(x => x.Account).ToListAsync();
        }

        /// <summary>
        /// Gets a customer by its Id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Customer> GetById(int customerId)
        {
            return await _context.Customer.Include(x => x.Account).FirstOrDefaultAsync(x => x.CustomerID == customerId);
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Customer> Update(int customerId, Customer request)
        {
            Customer customer = await GetById(customerId);
            if (customer != null)
            {
                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.PhoneNumber = request.PhoneNumber;
                customer.ZipCode = request.ZipCode;
                customer.Gender = request.Gender;
                customer.Modified_At = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            return customer;
        }
    }
}
