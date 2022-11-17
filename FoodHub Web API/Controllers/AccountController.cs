namespace FoodHub_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<StaticAccountResponse> accounts = await _accountService.GetAll();

                if (accounts == null)
                {
                    return Problem("Nothing was returned from service, this was unexpected");
                }

                if (accounts.Count == 0)
                {
                    return NoContent();
                }

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{accountId}")]
        public async Task<IActionResult> GetById(int accountId)
        {
            try
            {
                DirectAccountResponse directAccountResponse = await _accountService.GetById(accountId);

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

        [HttpPut]
        [Route("{accountId}")]
        public async Task<IActionResult> Update(int accountId, AccountRequest request)
        {
            try
            {
                DirectAccountResponse directAccountResponse = await _accountService.Update(accountId, request);

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
    }
}
