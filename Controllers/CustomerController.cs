using System.Threading.Tasks;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper;
using AduabaNeptune.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AduabaNeptune.Controllers
{
    [ApiController]
    [Route("Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public CustomerController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequest model)
        {
            var response = await _accountService.RegisterCustomerAsync(model);

            if (response == null)
            {
                return BadRequest(new {message = "Customer already exist"});
            }
            else
            {
                return CreatedAtAction(nameof(SignIn), response.AsRegistrationResponseDto());
            }
        }


        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody]SignInRequest model)
        {
            var response = await _accountService.SignInCustomerAsync(model);

            if (response == null)
            {
                return BadRequest(new {message = "Invalid Email or Password"});
            }
            else
            {
                return Ok(new {token = response});
            }
        }


        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDetails(UpdateCustomerRequest model)
        {

            var requesterIdentity = ClaimsProcessor.CheckClaimForEmail(HttpContext.User);

            if (requesterIdentity != null)
            {
                var response = await _accountService.UpdateCustomerDetailAsync(model, requesterIdentity);

                if (string.IsNullOrEmpty(response))
                {
                    return BadRequest(new { message = "Customer does not exist" });
                }
                else
                {
                    return Ok(new { message = "Customer details successfully updated", token = response });
                }
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}