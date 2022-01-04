using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities;
using Social.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Messages.Commands
{
    public class CreateMessageCommand : IRequest<Response<int>>
    {
        public int MatchId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
    }
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Response<int>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;
        public CreateMessageCommandHandler(IMessageRepository messageRepository, IMatchRepository matchRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _matchRepository = matchRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(request.MatchId);
            if(match == null || !match.IsMatch)
                throw new NotFoundException($"Match not found!!");

            var message = _mapper.Map<Message>(request);
            message.Status = MessageStatus.Sent;
            await _messageRepository.AddAsync(message);
            return new Response<int>(message.Id);
        }
    }
}
