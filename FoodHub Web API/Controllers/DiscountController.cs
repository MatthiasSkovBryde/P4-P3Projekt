namespace FoodHub_Web_API.Controllers
{
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<StaticDiscountResponse> responses = await _discountService.GetAll();

                if (responses == null)
                {
                    return Problem("The discount service responded with null.");
                }

                if (responses.Count == 0)
                {
                    return NoContent();
                }
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{discountId}")]
        public async Task<IActionResult> GetById(int discountId)
        {
            try
            {
                DirectDiscountResponse response = await _discountService.GetById(discountId);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DiscountRequest request)
        {
            try
            {
                DirectDiscountResponse response = await _discountService.Create(request);
                if (response == null)
                {
                    return BadRequest("Request can not be null");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{discountId}")]
        public async Task<IActionResult> Update( int discountId, [FromBody] DiscountRequest request)
        {
            try
            {
                DirectDiscountResponse response = await _discountService.Update(discountId, request);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{discountId}")]
        public async Task<IActionResult> Delete(int discountId)
        {
            try
            {
                DirectDiscountResponse response = await _discountService.Delete(discountId);
                if (response == null)
                {
                    return NotFound($"No Discount with the id: {discountId}");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
