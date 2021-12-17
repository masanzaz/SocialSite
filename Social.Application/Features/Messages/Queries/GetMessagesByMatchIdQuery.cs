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
    public class GetMessagesByMatchIdQuery : IRequest<PagedResponse<IEnumerable<MessagesViewModel>>>
    {
        public GetMessagesParameter parameter;
    }
    public class GetMessagesByMatchIdQueryHandler : IRequestHandler<GetMessagesByMatchIdQuery, PagedResponse<IEnumerable<MessagesViewModel>>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetMessagesByMatchIdQueryHandler(IMatchRepository matchRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<MessagesViewModel>>> Handle(GetMessagesByMatchIdQuery request, CancellationToken cancellationToken)
        {
            var conversation = await _matchRepository.GetByIdAsync(request.parameter.MatchId);
            if (conversation == null)
                throw new NotFoundException($"Conversation - {request.parameter.MatchId} not found!!");

            var messages = await _messageRepository.GetMessagesByConversationId(request.parameter.MatchId, request.parameter.PageNumber, request.parameter.PageSize);
            var conversationViewModel = _mapper.Map<IEnumerable<MessagesViewModel>>(messages);
            var totalRecords = await _messageRepository.CountByConversationId(request.parameter.MatchId);

            return new PagedResponse<IEnumerable<MessagesViewModel>>(conversationViewModel, request.parameter.PageNumber, request.parameter.PageSize, totalRecords);
        }
    }
}