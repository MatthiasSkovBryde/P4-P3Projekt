namespace FoodHub_Web_API.Controllers
{
    /// <summary>
    /// Using CustomerController to contol customer. Route is used to determine what there is in the URL.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Using ICustomerService as interface.
        /// </summary>
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Constuctor for CustomerController
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // List of StaticCustomerResponse as customers using the service.GetAll()
                List<StaticCustomerResponse> customers = await _customerService.GetAll();

                if (customers == null)
                {
                    return Problem("Nothing was returned from service, this was unexpected");
                }

                if (customers.Count == 0)
                {
                    return NoContent();
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Gets customer by its id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{customerId}")]
        [Authorize]
        public async Task<IActionResult> GetById(int customerId)
        {
            try
            {
                DirectCustomerResponse directAccountResponse = await _customerService.GetById(customerId);

                if (directAccountResponse == null)
                {
                    return NotFound();
                }
                return Ok(directAccountResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewCustomerRequest request)
        {
            try
            {
                DirectCustomerResponse directCustomerResponse = await _customerService.Create(request);

                if (directCustomerResponse == null)
                {
                    return Problem("Customer was not created, something failed...");
                }
                return Ok(directCustomerResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{customerId}")]
        [Authorize]
        public async Task<IActionResult> Update(int customerId, NewCustomerRequest request)
        {
            try
            {
                DirectCustomerResponse directCustomerResponse = await _customerService.Update(customerId, request);

                if (directCustomerResponse == null)
                {
                    return NotFound();
                }
                return Ok(directCustomerResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{customerId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int customerId)
        {
            try
            {
                DirectCustomerResponse directCustomerResponse = await _customerService.Delete(customerId);

                if (directCustomerResponse == null)
                {
                    return NotFound();
                }
                return Ok(directCustomerResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
