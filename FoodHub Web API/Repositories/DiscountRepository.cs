namespace FoodHub_Web_API.Repositories
{
    public interface IDiscountRepository
    {
        Task<List<Discount>> GetAll();
        Task<Discount> GetById(int discountId);
        Task<Discount> Create(Discount request);
        Task<Discount> Update(int discountId, Discount request);
        Task<Discount> Delete(int discountId);
    }

    public class DiscountRepository : IDiscountRepository
    {
        private readonly DatabaseContext _context;

        public DiscountRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Discount> Create(Discount request)
        {
            _context.Discount.Add(request);
            await _context.SaveChangesAsync();
            return await GetById(request.DiscountID);
        }

        public async Task<Discount> Delete(int discountId)
        {
            Discount discount = await GetById(discountId);
            if (discount != null)
            {
                _context.Discount.Remove(discount);
                await _context.SaveChangesAsync();
            }
            return discount;
        }

        public async Task<List<Discount>> GetAll()
        {
            return await _context.Discount.Include(x => x.Transactions).Include(x => x.Products).ToListAsync();
        }

        public async Task<Discount> GetById(int discountId)
        {
            return await _context.Discount.Include( x => x.Transactions).Include( x => x.Products).ThenInclude( x => x.Photos).FirstOrDefaultAsync(x => x.DiscountID == discountId);
        }

        public async Task<Discount> Update(int discountId, Discount request)
        {
            Discount discount = await GetById(discountId);
            if (discount != null)
            {
                discount.Name = request.Name;
                discount.Description = request.Description;
                discount.DiscountPercent = request.DiscountPercent;
                discount.Modified_At = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            return discount;
        }
    }
}
