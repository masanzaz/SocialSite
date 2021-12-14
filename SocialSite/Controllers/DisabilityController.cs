using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.Application.Features.Disabilities.Queries;
using Social.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialSite.Controllers
{
    [ApiVersion("1.0")]
    public class DisabilityController : BaseApiController
    {

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetDisabilities([FromQuery] RequestParameter filter)
        {

            return Ok(await Mediator.Send(new GetDisabilitiesAllQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDisabilityById(int id)
        {
            return Ok(await Mediator.Send(new GetDisabilityByIdQuery { Id = id }));
        }



    }
}
