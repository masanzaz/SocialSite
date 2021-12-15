using Microsoft.AspNetCore.Mvc;
using Social.Application.Features.Genres.Queries;
using System.Threading.Tasks;

namespace SocialSite.Controllers
{
    [ApiVersion("1.0")]
    public class GenreController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            return Ok(await Mediator.Send(new GetGenresAllQuery()));
        }
    }
}
