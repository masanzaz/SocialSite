﻿using Microsoft.AspNetCore.Mvc;
using Social.Application.Features.Matches.Queries;
using Social.Application.Features.Messages;
using Social.Application.Features.Persons.Commands;
using Social.Application.Features.Persons.Queries;
using Social.Application.Parameters;
using System.Threading.Tasks;


namespace SocialSite.Controllers
{
    [ApiVersion("1.0")]
    public class PersonController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterPerson(CreatePersonCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            return Ok(await Mediator.Send(new GetPersonByIdQuery { Id = id }));
        }

        [HttpGet("{get-matches}")]
        public async Task<IActionResult> GetMatchesByPersonId([FromQuery] GetMessagesParameter filter)
        {
            return Ok(await Mediator.Send(new GetMatchesByPersonIdQuery { parameter = filter }));
        }
    }
}
