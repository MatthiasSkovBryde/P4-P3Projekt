namespace FoodHub_Web_API.Services
{
    public interface IDiscountService
    {
        Task<List<StaticDiscountResponse>> GetAll();
        Task<DirectDiscountResponse> GetById(int discountId);
        Task<DirectDiscountResponse> Create(DiscountRequest request);
        Task<DirectDiscountResponse> Update(int discountId, DiscountRequest request);
        Task<DirectDiscountResponse> Delete(int discountId);
    }

    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<DirectDiscountResponse> Create(DiscountRequest request)
        {
            Discount discount = await _discountRepository.Create(_mapper.Map<Discount>(request));
            if (discount != null)
            {
                return _mapper.Map<DirectDiscountResponse>(discount);
            }
            return null;
        }

        public async Task<DirectDiscountResponse> Delete(int discountId)
        {
            Discount discount = await _discountRepository.Delete(discountId);
            if (discount != null)
            {
                return _mapper.Map<DirectDiscountResponse>(discount);
            }
            return null;
        }

        public async Task<List<StaticDiscountResponse>> GetAll()
        {
            List<Discount> discounts = await _discountRepository.GetAll();
            if (discounts != null)
            {
                return discounts.Select(discount => _mapper.Map<StaticDiscountResponse>(discount)).ToList();
            }
            return null;
        }

        public async Task<DirectDiscountResponse> GetById(int discountId)
        {
            Discount discount = await _discountRepository.GetById(discountId);
            if (discount != null)
            {
                DirectDiscountResponse mappedDiscount = _mapper.Map<DirectDiscountResponse>(discount);
                List<StaticProductResponse> mappedProducts = mappedDiscount.Products;
                foreach (var product in discount.Products)
                {
                    if (product.Photos.Count == 0)
                    {
                        continue;
                    }

                    var x = mappedProducts.Find(x => x.ProductID == product.ProductID);
                    if (x != null)
                    {
                        x.ImageName = product.Photos.First().ImageName;
                    }
                }
                return mappedDiscount;
            }
            return null;
        }

        public async Task<DirectDiscountResponse> Update(int discountId, DiscountRequest request)
        {
            Discount discount = await _discountRepository.Update(discountId, _mapper.Map<dynamic>(request));
            if (discount != null)
            {
                return _mapper.Map<DirectDiscountResponse>(discount);
            }
            return null;
        }
    }
}
