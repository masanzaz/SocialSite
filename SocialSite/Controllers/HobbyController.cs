using Microsoft.AspNetCore.Mvc;
using Social.Application.Features.Hobbies.Queries;
using System.Threading.Tasks;

namespace SocialSite.Controllers
{
    [ApiVersion("1.0")]
    public class HobbyController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllHobbies()
        {
            return Ok(await Mediator.Send(new GetHobbyAllQuery()));
        }
    }
}
