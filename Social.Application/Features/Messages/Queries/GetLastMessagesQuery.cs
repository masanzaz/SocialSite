using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Application.Parameters;
using Social.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Messages.Queries
{
  public class GetLastMessagesQuery : IRequest<PagedResponse<IEnumerable<LastMessagesViewModel>>>
    {
        public GetMessagesParameter parameter;
    }
    public class GetLastMessagesQueryHandler : IRequestHandler<GetLastMessagesQuery, PagedResponse<IEnumerable<LastMessagesViewModel>>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetLastMessagesQueryHandler(IMatchRepository matchRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<LastMessagesViewModel>>> Handle(GetLastMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetLastMessages(request.parameter.MatchId, request.parameter.PageNumber, request.parameter.PageSize);
            var totalRecords = await _messageRepository.CountByConversationId(request.parameter.MatchId);

            return new PagedResponse<IEnumerable<LastMessagesViewModel>>(messages, request.parameter.PageNumber, request.parameter.PageSize, totalRecords);
        }
    }
}