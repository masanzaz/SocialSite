using Microsoft.AspNetCore.Mvc;
using Social.Application.Features.Matches.Commands;
using Social.Application.Features.Messages;
using Social.Application.Features.Messages.Commands;
using Social.Application.Features.Messages.Queries;
using Social.Application.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSite.Controllers
{
    [ApiVersion("1.0")]
    public class ChatController : BaseApiController
    {

        [HttpPost("new-match")]
        public async Task<IActionResult> NewConversation(CreateMatchCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage(CreateMessageCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("get-conversations")]
        public async Task<IActionResult> GetConversations([FromQuery] GetMessagesParameter filter)
        {
            return Ok(await Mediator.Send(new GetMessagesByMatchIdQuery() { parameter = filter }));
        }

        [HttpGet("getLastMessages")]
        public async Task<IActionResult> GetLastMessages([FromQuery] GetMessagesParameter filter)
        {
            return Ok(await Mediator.Send(new GetLastMessagesQuery() { parameter = filter }));
        }


        [HttpDelete("cancel-match")]
        public async Task<IActionResult> CancelMatch(int id)
        {
            return Ok(await Mediator.Send(new CancelMatchCommand { MatchId = id }));
        }


    }
}
