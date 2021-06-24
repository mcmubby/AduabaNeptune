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
        public IActionResult Register([FromBody]RegistrationRequest model)
        {
            if (model == null)
            {
                return BadRequest(new {message = "Request model is empty or incomplete"});
            }

            var response = _accountService.RegisterCustomer(model);

            if (response == false)
            {
                return BadRequest(new {message = "Customer already exist"});
            }
            else
            {
                return Ok(new {message = "Customer successfully registered"});
            }
        }


        [HttpPost("signin")]
        public IActionResult SignIn([FromBody]SignInRequest model)
        {
            if (model == null)
            {
                return BadRequest(new {message = "Request model is empty or incomplete"});
            }

            var response = _accountService.SignInCustomer(model);

            if (response == null)
            {
                return BadRequest(new {message = "Invalid Email or Password"});
            }
            else
            {
                return Ok(new {message = "Customer successfully signed-in", token = response});
            }
        }


        [Authorize]
        [HttpPut("update")]
        public IActionResult UpdateDetails(UpdateCustomerRequest model)
        {
            if (model == null)
            {
                return BadRequest(new {message = "Request model is empty or incomplete"});
            }

            var requesterIdentity = HttpContext.User;

            if (requesterIdentity.HasClaim(c => c.Type == CustomClaimType.Email.ToString()))
            {
                var requesterClaim = requesterIdentity.FindFirst(c => c.Type == CustomClaimType.Email.ToString());
                var response = _accountService.UpdateCustomerDetail(model, requesterClaim);

                if (string.IsNullOrEmpty(response))
                {
                    return BadRequest(new { message = "Customer does not exist" });
                }
                else
                {
                    return Ok(new { message = "Customer details successfully updated", newToken = response });
                }
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}