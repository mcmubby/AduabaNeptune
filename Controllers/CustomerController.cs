using System.Net;
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
        private readonly IMessageSenderService _messageSenderService;

        public CustomerController(IAccountService accountService, IMessageSenderService messageSenderService)
        {
            _accountService = accountService;
            _messageSenderService = messageSenderService;
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


        [HttpPost("check-email")]
        public async Task<IActionResult> CheckForExistingCustomer([FromBody]string emailAddress)
        {
            var response = await _accountService.GetCustomerByEmail(emailAddress);

            if(response == null){return BadRequest(new {message = "Customer doesn't exist"});}

            return Ok();
        }


        [HttpPost("send-reset-pin")]
        public async Task<IActionResult> SendPasswordResetPin([FromBody]PasswordResetPinRequest requestDetails)
        {
            var customer = await _accountService.GetCustomerByEmail(requestDetails.CustomerEmail);

            if(customer == null){return BadRequest(new {message = "Customer doesn't exist"});}

            if(requestDetails.SendToEmail == true)
            {
                await _messageSenderService.SendPasswordResetTokenEmail(customer.Email, customer.Id);
                return Ok();
            }

            await _messageSenderService.SendPasswordResetTokenSms(customer.PhoneNumber, customer.Id);
            
            return Ok();
        }

        [HttpPost("verify-reset-pin")]
        public async Task<IActionResult> VerifyResetPin([FromBody]PinVerificationRequest request)
        {
            var response = await _accountService.VerifyResetPinAsync(request.Pin, request.Email);

            if(response == false){return BadRequest(new {message = "Pin verification failed"});}

            return Ok();
        }

        [HttpPatch("update-password")]
        public async Task<IActionResult> UpdateCustomerPassword([FromBody]UpdatePasswordRequest request)
        {
            var response = await _accountService.UpdateCustomerPassword(request.Email, request.Password);

            if(response == false){return BadRequest();}

            return Ok();
        }


        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody]SignInRequest model)
        {
            var response = await _accountService.SignInCustomerAsync(model);

            if (response == null)
            {
                return BadRequest(new {message = "Credentials doesn't exist"});
            }
            else
            {
                return Ok(new {token = response});
            }
        }


        [Authorize]
        [HttpPatch("update-customer-details")]
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