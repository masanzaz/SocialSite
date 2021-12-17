using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Matches.Commands
{
  public class CreateMatchCommand : IRequest<Response<int>>
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
    public class CreateMatchCommandCommandHandler : IRequestHandler<CreateMatchCommand, Response<int>>
    {
        private readonly IMatchRepository _matchRepository;
        IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public CreateMatchCommandCommandHandler(IMatchRepository matchRepository, IPersonRepository personRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var reciver = await _personRepository.GetByIdAsync(request.ReceiverId);
            if (reciver == null)
                throw new NotFoundException($"Receiver - {request.ReceiverId} not found!!");

            var sender = await _personRepository.GetByIdAsync(request.SenderId);
            if (sender == null)
                throw new NotFoundException($"Sender - {request.SenderId} not found!!");

            if (request.SenderId == request.ReceiverId)
                throw new BadRequestException($"Reciver is incorrent!!");

            var match = await _matchRepository.GetBySenderAsync(request.SenderId, request.ReceiverId);
            if (match != null)
                return new Response<int>(match.Id);

            match = await _matchRepository.GetBySenderAsync(request.ReceiverId, request.SenderId);
            if (match != null)
            {
                match.IsMatch = true;
                await _matchRepository.UpdateAsync(match);
                return new Response<int>(match.Id);
            }

            match = _mapper.Map<Match>(request);
            match.IsMatch = false;
            await _matchRepository.AddAsync(match);

            return new Response<int>(match.Id);
        }
    }
}
