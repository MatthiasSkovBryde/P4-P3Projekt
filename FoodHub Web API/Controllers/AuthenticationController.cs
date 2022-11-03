namespace FoodHub_Web_API.Controllers
{
    /// <summary>
    /// Using AuthenticationController to authenticate.
    /// </summary>
    [Route("Api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authenticationService"></param>
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Used to check if you have a response from AuthenticationResponse. If you have a response, then you are authenticated and we set your refreshtoken in the browser cookies.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>response or an error</returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request) // We use [FromBody] for security measures
        {
            try
            {
                AuthenticationResponse response = await _authenticationService.Authenticate(request, IPAddress());

                if (response == null)
                {
                    // Returns statuscode 401 unautherized
                    return Unauthorized("Incorrect Email or Password");
                }

                //Calling the SetTokenCookie method to set the cookies in the client browser, and makes them authenticated.
                SetTokenCookie(response.RefreshToken);

                // Returns statuscode 200 Ok
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Used to check if you have a refreshtoken. 
        /// </summary>
        /// <returns>response or an error</returns>
        [HttpPut]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                // Trying to get a refreshtoken in the clients browser and putting it in a veriable.
                string refreshToken = Request.Cookies["refreshtoken"];

                if (string.IsNullOrEmpty(refreshToken))
                {
                    // If there is no or null, then we return badrequest.
                    return BadRequest("Missing refresh token!");
                }

                AuthenticationResponse response = await _authenticationService.RefreshToken(refreshToken, IPAddress());

                if (response == null)
                {
                    return Problem("An unexpected error occured, please try again hej");
                }

                SetTokenCookie(response.RefreshToken);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RevokeToken()
        {
            try
            {
                string refreshToken = Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(refreshToken))
                {
                    return BadRequest("Missing refresh token!");
                }

                bool result = await _authenticationService.RevokeToken(refreshToken, IPAddress());

                if (!result)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Used to use cookies in your browser.
        /// </summary>
        /// <param name="token"></param>
        private void SetTokenCookie(string token)
        {
            // Configure cookie, setting expiration date and enabling HttpOnly
            CookieOptions options = new()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            // Append the token to the cookies of the current response from the server
            Response.Cookies.Append("refreshToken", token, options);
        }

        /// <summary>
        /// Getting your IP, so we can use it to authenticate.
        /// </summary>
        /// <returns>IP</returns>
        private string IPAddress()
        {
            // Check if the http request contains an IP if not get it from the context instead
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                // Get the IP from the request
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                // Get the IP and map it to an IPv4
                return HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            }
        }
    }
}