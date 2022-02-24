using Microsoft.AspNetCore.Mvc;
using Social.Application.Features.Persons.Commands;
using Social.Application.Features.Users.Commands;
using System.Threading.Tasks;

namespace SocialSite.Controllers.Auth
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("ConfirmPhoneNumber")]
        public async Task<IActionResult> ConfirmPhoneNumber(ConfirmPhoneNumberCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("ValidatePhoneNumber")]
        public async Task<IActionResult> ValidatePhoneNumber(ValidatePhoneNumberCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
